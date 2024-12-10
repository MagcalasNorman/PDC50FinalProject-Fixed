using PDC50FinalProject.Model;
using PDC50FinalProject.ViewModel;

namespace PDC50FinalProject.View;

public partial class StudentList : ContentPage
{
	public StudentList()
	{
		InitializeComponent();
		BindingContext = new StudentViewModel();
	}

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Student selectedStudent)
        {
            await Navigation.PushAsync(new StudentDetailsPage(selectedStudent));
        }
    }
}