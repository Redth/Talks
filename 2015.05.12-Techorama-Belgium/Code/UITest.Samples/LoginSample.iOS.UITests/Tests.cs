using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.iOS;
using Xamarin.UITest.Queries;
using LoginSample.Shared;

[assembly: Xamarin.UITest.TestCloudApiKey(Constants.TEST_CLOUD_API_KEY)]

namespace LoginSample.iOS.UITests
{
    [TestFixture]
    public class Tests
    {
        iOSApp app;

        [SetUp]
        public void BeforeEachTest ()
        {
            app = ConfigureApp
				.iOS
                .EnableLocalScreenshots ()
            	.StartApp ();
        }

        #region 1. REPL
        [Test]
        public void Repl ()
        {           
            app.Repl ();
        }
        #endregion

        #region 2. Login Success
        [Test]
        public void Test01_LoginSuccess ()
        {                    
            app.Screenshot ("Launch");

            // Enter the email
            app.Tap(e => e.Marked ("textEmail"));
            app.EnterText("foo@user.com");
            app.Screenshot ("Enter Email");

            // Enter the password
            app.Tap(e => e.Marked ("textPassword"));
            app.EnterText("bar");
            app.Screenshot ("Enter Password");

            // Tap login button
            app.PressEnter ();
            app.Screenshot ("Press Enter");

            // Wait for next activity to confirm success
            app.WaitForElement (q => q.Text ("Home"));            

            app.Screenshot ("Logged In");           
        }
        #endregion

        #region 3. Login Failed
        [Test]
        public void Test02_LoginFailed ()
        {
            app.Screenshot ("Launch");

            // Enter the email
            app.Tap(e => e.Marked ("textEmail"));
            app.EnterText("blah");
            app.Screenshot ("Enter Email");

            // Enter the password
            app.Tap(e => e.Marked ("textPassword"));
            app.EnterText("blah");           
            app.Screenshot ("Enter Password");

            // Tap login button
            app.PressEnter ();
            app.Screenshot ("Press Enter");

            // Wait for message to show us login failed
            app.WaitForElement (q => q.Text ("Login Failed"));

            app.Screenshot ("Login Failed");
        }
        #endregion

        #region Filter Results
        [Test]
        public void Test03_FilterResults ()
        {            
            // Login first
            Test01_LoginSuccess ();

            // Get the count of items (this will list visible items)
            var itemCount = app.Query(q => q.Class("UITableViewCell")).Count ();

            // Make sure there are more than zero
            Assert.Greater (itemCount, 0, "No items in the list");

            // Enter a search term
            app.Tap (c => c.Class ("UISearchBarTextField"));
            app.EnterText ("twentyt");

            // Get the count after filtering has happened
            itemCount = app.Query(q => q.Class("UITableViewCell")).Count ();

            // We should see two items
            Assert.AreEqual (itemCount, 2);

            app.Screenshot ("Filter Search");
        }
        #endregion
    }
}

