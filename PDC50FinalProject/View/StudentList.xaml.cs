using PDC50FinalProject.Model;
using PDC50FinalProject.ViewModel;
using System.Diagnostics;

namespace PDC50FinalProject.View;

public partial class StudentList : ContentPage
{
    private readonly StudentViewModel _viewModel;

    public StudentList()
    {
        InitializeComponent();
        _viewModel = new StudentViewModel(); // Initialize your ViewModel
        BindingContext = _viewModel; // Set the BindingContext to the ViewModel
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Load the students when the page is appearing
        await _viewModel.LoadStudents();
    }

    // Handle the item tapped event to navigate to the StudentDetailsPage
    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        try
        {
            if (e.Item is Student selectedStudent && selectedStudent != null)
            {
                await Navigation.PushAsync(new StudentDetailsPage(selectedStudent));
            }
            else
            {
                Debug.WriteLine("Selected student is invalid or null.");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception occurred during navigation: {ex.Message}");
        }
    }



}