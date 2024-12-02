using MVVM.Command;
using MVVM.Models;
using System.Collections.ObjectModel;


namespace MVVM.ViewModels
{
    public class HobbyViewModel : ViewModelBase
    {
        //Backing variabel
        private ObservableCollection<HobbyItemViewModel> hobbies = new();
        private HobbyItemViewModel selectedHobby;

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
            get { return selectedHobby; }
            set
            {
                selectedHobby = value;
                RaisePropertyChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand AddCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public HobbyViewModel()
        {
            hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Baka pepparkakor" }));
            hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Dricka glögg" }));
            hobbies.Add(new HobbyItemViewModel(new Hobby() { Name = "Klä granar" }));

            AddCommand = new DelegateCommand(AddHobby);
            DeleteCommand = new DelegateCommand(DeleteHobby, CanDelete);
        }

        private bool CanDelete(object? parameter) => SelectedHobby != null;
       

        private void DeleteHobby(object? parameter)
        {
            if(SelectedHobby != null)
            {
                Hobbies.Remove(SelectedHobby); // lägger till ny hobby
                SelectedHobby = null;
            }
        }

        public async Task LoadAsync()
        {
            if (Hobbies.Any())
            {
                return;
            }
        }
        private void AddHobby(object? parameter)
        {
            Hobby hobby = new Hobby() { Name = "Ny hobby" };
            var hobbyVM = new HobbyItemViewModel(hobby);
            Hobbies.Add(hobbyVM); // lägger till ny hobby
            SelectedHobby = hobbyVM; // gör så att hobbyn dyker upp på sidan när vi skapa
        }
    }
}
