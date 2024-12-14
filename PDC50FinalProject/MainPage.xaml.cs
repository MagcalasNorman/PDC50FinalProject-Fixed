namespace PDC50FinalProject
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Hard-coded username and password for validation
            string correctUsername = "SirFer";
            string correctPassword = "SirFer";

            // Get the input values from the Entry fields
            string enteredUsername = UsernameEntry.Text;
            string enteredPassword = PasswordEntry.Text;

            // Check if the entered credentials match the hard-coded values
            if (enteredUsername == correctUsername && enteredPassword == correctPassword)
            {
                // Logic for successful login
                await DisplayAlert("Login", "Logging in...", "OK");

                // Navigate to the Home page (you can change "HomePage" to the correct route or page name)
                await Shell.Current.GoToAsync("//HomePage");
            }
            else
            {
                // Logic for incorrect credentials
                await DisplayAlert("Error", "Wrong credentials, please try again.", "OK");
            }
        }

        private async void OnCreateAccountClicked(object sender, EventArgs e)
        {
            // Logic to navigate to create account page (if you have one)
            await DisplayAlert("Create Account", "Redirecting to create account...", "OK");
            // Replace with actual navigation if needed
        }

        // Optional method to toggle password visibility
        private void TogglePasswordVisibility(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
        }
    }
}
