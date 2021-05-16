using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using TestREST_210422.Data;
using System.Collections.ObjectModel;

namespace TestREST_210422
{
    public partial class MainPage : ContentPage
    {
        readonly IList<Test> tests = new ObservableCollection<Test>();
        readonly TestManager testManager = new TestManager();

        public MainPage()
        {
            //BindingContext = books;
            BindingContext = tests;
            InitializeComponent();
        }

        async void OnAddNewTest(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(
                new AddEditTestPage(testManager, tests));
        }

        async void OnRefreshTest(object sender, EventArgs e)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var testCollection = await testManager.GetAll();
                foreach (Test test in testCollection)
                {
                    if (tests.All(a => a.EventId != test.EventId))
                        tests.Add(test);

                }
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnEditTest(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushModalAsync(new AddEditTestPage(testManager, tests, (Test)e.Item));
        }
    }
}
