namespace NoteApp.Views;

public partial class AllNotesPage : ContentPage
{
	public AllNotesPage()
	{
		InitializeComponent();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
		// Clear the selected item, so we can select the item again later after going back from NotePage
		NotesCollection.SelectedItem = null;
    }
}