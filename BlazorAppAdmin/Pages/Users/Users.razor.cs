using BlazorAppAdmin.Services;
using Microsoft.AspNetCore.Components;
using SharedLib.AdminDTO;
using SharedLib.DTO;

namespace BlazorAppAdmin.Pages.Users
{
    public partial class Users
    {
        public bool AccordionShow { get; set; } = false;
        [Inject]
        public UsersService UsersService { get; set; }
        public IEnumerable<UserDTO> UsersList { get; set; }

        public int Count { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;
        public int AllCount { get; private set; } 
        public string? HasOrder { get; set; } = null;
        public string? Qstring { get; private set; } = null;
        public string? SearchBy { get; private set; } = "Username";

        public void ToggleAccordion()=> AccordionShow = !AccordionShow;


        private UserQueryDTO BuildQuery() =>
         new()
         {
             Count = Count,
             PageNum = CurrentPage,
             Qstring = Qstring,
             HasOrder = HasOrder,
             SearchBy = SearchBy
         };

        private async Task LoadUsers()
        {
            var response = await UsersService.GetUsers(BuildQuery());
            if (response != null) {
                if (response.Success)
                {
                    UsersList = response.Data;
                    AllCount = response.Count;
                }
            }

        }

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();

        }

        private async Task ChangeSearchBy(string searchBy)
        {
            CurrentPage = 1;
            this.SearchBy = searchBy;
            await LoadUsers();
        }
        private async Task ChangeCount(int count)
        {
            CurrentPage = 1;
            this.Count = count;
            await LoadUsers();
        }
        private async Task Search()
        {
            CurrentPage = 1;
            await LoadUsers();

        }

        public async Task ChangePage(int page)
        {
            if (page > 0 && page <= (int)Math.Ceiling((double)AllCount / Count))
            {
                CurrentPage = page;
                await LoadUsers();
            }
        }

        public async Task PreviousPage() => await ChangePage(CurrentPage - 1);

        public async Task NextPage() => await ChangePage(CurrentPage + 1);
    }
}
