﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SMETA.Web;
using SMETA.Web.Controllers;
using SMETA.DataAccess.Repositories;
using SMETA.DataAccess.Models;
using SMETA.Web.Services;

namespace SMETA.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void SearchFilterTest()
        {
            PostRepository postRepository = new PostRepository();
            PostFilter filter = new PostFilter();
            int unfilteredResultsCount = postRepository.GetPosts(filter).Count();

            filter.Query = "Trump";
            int filteredResultsCount = postRepository.GetPosts(filter).Count();

            Assert.IsTrue(unfilteredResultsCount > filteredResultsCount);
        }

        [TestMethod]
        public void DateFilterTest()
        {
            PostRepository postRepository = new PostRepository();
            PostFilter filter = new PostFilter();
            int unfilteredResultsCount = postRepository.GetPosts(filter).Count();

            filter.StartDate = DateTime.Today;
            int filteredResultsCount = postRepository.GetPosts(filter).Count();

            Assert.IsTrue(unfilteredResultsCount > filteredResultsCount);
        }

        [TestMethod]
        public void UsernameFilterTest()
        {
            PostRepository postRepository = new PostRepository();
            PostFilter filter = new PostFilter();
            int unfilteredResultsCount = postRepository.GetPosts(filter).Count();

            filter.Username = "therealdonaldtrump";
            int filteredResultsCount = postRepository.GetPosts(filter).Count();

            Assert.IsTrue(unfilteredResultsCount > filteredResultsCount);
        }

        [TestMethod]
        public void IntervalFilterTest()
        {
            PostRepository postRepository = new PostRepository();
            PostFilter filter = new PostFilter();
            int unfilteredResultsCount = GroupingService.GroupByHour(postRepository.GetPosts(filter)).Count();
            
            int filteredResultsCount = GroupingService.GroupByDay(postRepository.GetPosts(filter)).Count();

            Assert.IsTrue(unfilteredResultsCount > filteredResultsCount);
        }
    }
}
