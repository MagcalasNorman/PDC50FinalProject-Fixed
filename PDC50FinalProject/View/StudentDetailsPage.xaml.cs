using PDC50FinalProject.Model;
using PDC50FinalProject.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace PDC50FinalProject.View;

public partial class StudentDetailsPage : ContentPage
{
    public StudentDetailsPage(Student selectedStudent)
    {
        InitializeComponent();
        if (selectedStudent == null)
        {
            Debug.WriteLine("Error: Selected student is null");
        }
        else

            // Set the BindingContext to the ViewModel and pass the selected student
            BindingContext = new StudentViewModel
        {
            SelectedStudent = selectedStudent
        };

        // Load academic history (optional, based on if it's already part of the selected student)
        LoadAcademicHistory();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is StudentViewModel viewModel)
        {
            // Explicitly await LoadAcademicHistoryAsync to ensure data is loaded before the UI is shown
            await viewModel.LoadAcademicHistoryAsync();
        }
    }

    private async void LoadAcademicHistory()
    {
      
            if (BindingContext is StudentViewModel viewModel)
            {
                // Use the ViewModel to load academic history for the selected student
                await viewModel.LoadAcademicHistoryAsync(); // Call without the student argument
            }
    }

    private async void OnGoBackButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    
}
