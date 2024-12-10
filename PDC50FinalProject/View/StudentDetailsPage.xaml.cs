using PDC50FinalProject.Model;
using PDC50FinalProject.ViewModel;
namespace PDC50FinalProject.View;


public partial class StudentDetailsPage : ContentPage
{
	public StudentDetailsPage(Student selectedStudent)
	{
		InitializeComponent();
		BindingContext = selectedStudent;
	}
    private async void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void OnEditButtonClicked(object sender, EventArgs e)
    {
        var selectedStudent = BindingContext as Student;
        if (selectedStudent != null)
        {
            
        }
    }
}