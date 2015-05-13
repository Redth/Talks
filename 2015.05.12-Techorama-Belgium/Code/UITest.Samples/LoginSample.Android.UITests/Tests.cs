using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Android;
using Xamarin.UITest.Queries;
using LoginSample.Shared;

[assembly: Xamarin.UITest.TestCloudApiKey(Constants.TEST_CLOUD_API_KEY)]

namespace LoginSample.UITests
{
    [TestFixture]
    public class Tests
    {
        AndroidApp app;

        [SetUp]
        public void BeforeEachTest ()
        {
            app = ConfigureApp
                .Android
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
            app.Tap(e => e.Id("editTextEmail"));
            app.EnterText("foo@user.com");
            app.Screenshot ("Enter Email");

            // Enter the password
            app.Tap(e => e.Id ("editTextPassword"));
            app.EnterText("bar");
            app.Screenshot ("Enter Password");

            // Tap login button
            app.Tap (e => e.Id ("buttonLogin"));
            app.Screenshot ("Tap Login");

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
            app.Tap(e => e.Id("editTextEmail"));
            app.EnterText("blah");

            // Enter the password
            app.Tap(e => e.Id ("editTextPassword"));
            app.EnterText("blah");

            // Tap login button
            app.Tap(q => q.Id("buttonLogin"));
            app.Screenshot ("Tap Login");

            // Wait for login failed message
            app.WaitForElement (q => q.Text ("Login Failed"));

            app.Screenshot ("Login Failed");
        }
        #endregion

        #region 4. Filter Results
        [Test]
        public void Test03_FilterResults ()
        {            
            // Login first
            Test01_LoginSuccess ();

            // Get the count of items (this will list visible items)
            var itemCount = app.Query(q => q.Id("text1")).Count ();

            // Make sure there are more than zero
            Assert.Greater (itemCount, 0, "No items in the list");

            // Enter a search term
            app.EnterText (q => q.Id ("search"), "twentyt");

            // Get the count after filtering has happened
            itemCount = app.Query(q => q.Id("text1")).Count ();

            // We should see two items
            Assert.AreEqual (itemCount, 2);

            app.Screenshot ("Filter Search");
        }
        #endregion
    }
}

