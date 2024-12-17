using MVVM.Command;
using MVVM.Models;
using System.Collections.ObjectModel;

namespace MVVM.ViewModels
{
    public class HobbyViewModel : ViewModelBase
    {     
        private ObservableCollection<HobbyItemViewModel> hobbies = new();
        private readonly ObservableCollection<HobbyItemViewModel> unfilteredHobbies;
        private HobbyItemViewModel selectedHobby;
        private string userInput;

        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public ObservableCollection<HobbyItemViewModel> Hobbies
        {
            get { return hobbies; }
            set
            {
                hobbies = value;
                RaisePropertyChanged();
            }
        }
        public HobbyItemViewModel SelectedHobby
        {
            get
            {
                return selectedHobby;
            }
            set
            {
                selectedHobby = value;
                RaisePropertyChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        public string UserInput
        {
            get { return userInput; }
            set
            {
                if (userInput != value)
                {
                    userInput = value;
                    RaisePropertyChanged();
                    FilterHobby(); // direkt filtrering när texten ändras
                }
            }
        }
        public HobbyViewModel()
        {
            hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Baka pepparkakor" }));
            hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Dricka glögg" }));
            hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Klä granar" }));
            unfilteredHobbies = hobbies;
            AddCommand = new DelegateCommand(AddHobby);
            DeleteCommand = new DelegateCommand(DeleteHobby, CanDelete);
        }
        private void FilterHobby()
        {
            if (string.IsNullOrWhiteSpace(UserInput))
            {
                Hobbies = unfilteredHobbies;
            }
            else
            {
                var filteredHobbies = Hobbies.Where(n => n.Name.Contains(UserInput, StringComparison.OrdinalIgnoreCase)).ToList();
                Hobbies = new ObservableCollection<HobbyItemViewModel>(filteredHobbies);
            }
        }
        private bool CanDelete(object? parameter) => SelectedHobby != null;

        private void DeleteHobby(object? parameter)
        {
            if (SelectedHobby != null)
            {
                Hobbies.Remove(SelectedHobby);
                SelectedHobby = null;
            }
        }
        public async Task LoadAsync()
        {
            if (Hobbies.Any())
            {
                await Task.CompletedTask;
                return;
            }
        }
        private void AddHobby(object? parameter)
        {
            Hobby hobby = new Hobby() { Name = "Ny hobby" };
            var hobbyVM = new HobbyItemViewModel(hobby);
            Hobbies.Add(hobbyVM);
            SelectedHobby = hobbyVM;
        }
    }
}