﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Bongo.Web
{
    [TestFixture]
    public class RoomBookingControllerTests
    {
        private Mock<IStudyRoomBookingService> _studyRoomBookingService;
        private RoomBookingController _bookingController;

        [SetUp]
        public void Setup()
        {
            _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
            _bookingController = new RoomBookingController(_studyRoomBookingService.Object);
        }

        [Test]
        public void IndexPage_CallRequest_VerifyGetAllInvoked()
        {
            _bookingController.Index();
            _studyRoomBookingService.Verify(x => x.GetAllBooking(), Times.Once);
        }

        [Test]
        public void BookRoomCheck_ModelStateInvalid_ReturnView()
        {
            _bookingController.ModelState.AddModelError("test", "test");

            var studyRoom = new StudyRoomBooking();

            var result = _bookingController.Book(studyRoom);

            ViewResult viewResult = result as ViewResult;
            ClassicAssert.AreEqual("Book", viewResult.ViewName);
        }

        [Test]
        public void BookRoomCheck_NotSuccessful_NoRoomCode()
        {
            var studyRoomBookingResult = new StudyRoomBookingResult()
            {
                Code = StudyRoomBookingCode.NoRoomAvailable
            };

            var studyRoom = new StudyRoomBooking();


            _studyRoomBookingService.Setup(x =>
            x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns(studyRoomBookingResult);

            var result = _bookingController.Book(studyRoom);
            ClassicAssert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult;
            ClassicAssert.AreEqual("No Study Room available for selected date"
                , viewResult.ViewData["Error"]);

        }

        [Test]
        public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        {
            //arrage
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.Success,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    Date = booking.Date,
                    Email = booking.Email
                });
            //act
            var result = _bookingController.Book(new StudyRoomBooking()
            {
                Date = DateTime.Now,
                Email = "hello@dotnetmastery.com",
                FirstName = "Hello",
                LastName = "DotNetMastery",
                StudyRoomId = 1
            });

            //assert
            ClassicAssert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult actionResult = result as RedirectToActionResult;
            ClassicAssert.AreEqual("Hello", actionResult.RouteValues["FirstName"]);
            ClassicAssert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
        }
    }
}
