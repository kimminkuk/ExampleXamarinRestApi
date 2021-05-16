using Xamarin.Forms;
using System;
using System.Linq;
using TestREST_210422.Data;
using System.Collections.Generic;

namespace TestREST_210422
{
    public class AddEditTestPage : ContentPage
    {
        readonly Test existingTest;
        readonly EntryCell eventTitleCell, eventDescriptionCell, avenueCell;
        readonly IList<Test> tests;
        readonly TestManager manager;
        public AddEditTestPage(TestManager manager, IList<Test> tests, Test existingTest = null)
        {
            this.manager = manager;
            this.tests = tests;
            this.existingTest = existingTest;

            var tableView = new TableView
            {
                Intent = TableIntent.Form,
                Root = new TableRoot(existingTest != null ? "Edit Test" : "New Test")
                {
                    new TableSection("Details")
                    {
                        //new TextCell
                        //{
                        //    Text = "EventId",
                        //    Detail = (existingTest != null) ? existingTest.eventId : "Will be generate"
                        //},
                        (eventTitleCell = new EntryCell {
                            Label = "EventTitle",
                            Placeholder = "add EventTitle",
                            Text = (existingTest != null) ? existingTest.EventTitle : null,
                        }),
                        (eventDescriptionCell = new EntryCell {
                            Label = "EventDescription",
                            Placeholder = "add EventDescription",
                            Text = (existingTest != null) ? existingTest.EventDescription : null,
                        }),
                        (avenueCell = new EntryCell {
                            Label = "Avenue",
                            Placeholder = "add Avenue",
                            Text = (existingTest != null) ? existingTest.Avenue : null,
                        }),


                    },
                }
            };

            Button button = new Button()
            {
                BackgroundColor = existingTest != null ? Color.Gray : Color.Green,
                TextColor = Color.White,
                Text = existingTest != null ? "Finished" : "Add Test",
                CornerRadius = 0,
            };
            button.Clicked += OnDismiss;

            Content = new StackLayout
            {
                Spacing = 0,
                Children = { tableView, button },
            };
        }

        async void OnDismiss(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            try
            {
                string eventTitle = eventTitleCell.Text;
                string eventDescription = eventDescriptionCell.Text;
                string avenue = avenueCell.Text;
                
                if(string.IsNullOrWhiteSpace(eventTitle) 
                    || string.IsNullOrWhiteSpace(eventDescription)
                    || string.IsNullOrWhiteSpace(avenue))
                {
                    await this.DisplayAlert("Missing Information",
                        "You must enter values for the EventTitle,EventDescription,Avenue",
                        "OK");
                }
                else
                {
                    var test = await manager.Add(eventTitle, eventDescription, avenue);
                }
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch(Exception ex)
            {
                await this.DisplayAlert("Error",
                    ex.Message,
                    "OK");
            }
            finally
            {
                IsBusy = false;
                button.IsEnabled = true;
            }
        }
    }
}