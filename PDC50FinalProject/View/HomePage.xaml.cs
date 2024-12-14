namespace PDC50FinalProject.View;

public partial class HomePage : ContentPage
{
    public HomePage()
    {
        InitializeComponent(); // Calls the generated method from the XAML file
    }

    private async void ClickedViewStudent(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//StudentList");
    }
}

