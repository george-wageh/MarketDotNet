using MarketApi.IRepositories;
using MarketApi.Models;
using MarketApi.Repositories;
using MarketApi.UnitWork;
using Microsoft.AspNetCore.Identity;
using SharedLib.DTO;

namespace MarketApi.Services
{
    public class CommentService
    {
        public CommentService(ICommentRepository commentRepository, IProductsRepository ProductRepository , UnitWorkApp UnitWorkApp)
        {
            CommentRepository = commentRepository;
            this.ProductRepository = ProductRepository;
            this.UnitWorkApp = UnitWorkApp;
        }

        private ICommentRepository CommentRepository { get; }
        private IProductsRepository ProductRepository { get; }
        public UnitWorkApp UnitWorkApp { get; }

        public async Task<ResponseDTO<IEnumerable<CommentDTO>>> GetAllAsync(int productId)
        {
            var comments = await CommentRepository.GetAllWithUsersAsync(productId);
            return new ResponseDTO<IEnumerable<CommentDTO>>
            {
                Success = true,
                Message = "",
                Data = comments.Select(x => new CommentDTO
                {
                    CommentText = x.Content,
                    CommentTitle = x.Title,
                    UserName = x.User?.FullName ?? "Unknown"
                }).ToList()
            };
        }
        public async Task<ResponseDTO<CommentDTO>> AddAsync(int productId, string userId, CommentDTO commentDTO)
        {
            if ((await ProductRepository.ExistsProductAsync(productId)))
            {
                var commentDb = new Comment
                {
                    ProductId = productId,
                    UserId = userId,
                    Title = commentDTO.CommentTitle,
                    Content = commentDTO.CommentText
                };
                await CommentRepository.AddAsync(commentDb);
                await UnitWorkApp.SaveChangesAsync();
                commentDTO.Id = commentDb.Id;
                return new ResponseDTO<CommentDTO>
                {
                    Success = true,
                    Message = "Comment Saved",
                    Data = commentDTO
                };
            }
            else {
                return new ResponseDTO<CommentDTO>
                {
                    Success = false,
                    Message = "Product not found",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<object>> DeleteAsync(string userId ,  int commentId) {
            var comment = await CommentRepository.GetAsync(commentId);
            if (comment != null)
            {
                if (comment.UserId == userId)
                {
                    CommentRepository.Delete(comment);
                    await UnitWorkApp.SaveChangesAsync();
                    return new ResponseDTO<object>
                    {
                        Message = "",
                        Data = "",
                        Success = true
                    };

                }
                else {
                    return new ResponseDTO<object>
                    {
                        Message = "no access to delete this comment",
                        Data = "",
                        Success = false
                    };
                }
            }
            else {
                return new ResponseDTO<object>
                {
                    Message = "Comment not found",
                    Data = "",
                    Success = false,
                };
            }
        }

    }
}
