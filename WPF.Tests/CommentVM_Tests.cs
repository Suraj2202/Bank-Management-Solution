using BankManagement_WPF.ViewModel;
using BankManagement_WPF.ViewModel.Commands;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Tests
{
    [TestFixture]
    class CommentVM_Tests
    {
        private CommentVM commentVM;
        private CommentCloseCommand commentCloseCommand;

        [SetUp]
        public void Setup()
        {
            commentVM = new CommentVM();
            commentCloseCommand = new CommentCloseCommand(commentVM);
        }

        [Test]
        public void CloseWindow_WithoutComment_Test()
        {
            commentCloseCommand.CanExecute(null);
            commentCloseCommand.Execute(null);

            Assert.IsNotNull(commentVM.Warning);
        }

        [Test]
        public void CloseWindow_WithComment_Test()
        {
            commentVM.Comment = "Not a problem";
            commentCloseCommand.CanExecute(null);
            commentCloseCommand.Execute(null);

            Assert.IsNull(commentVM.Warning);
        }
    }
}
