﻿using System;
using Bibuddy.DataAccess.Concrete;
using Bibuddy.DataAccess.Concrete.Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bibuddy.DataAccess.Test.EF
{
    [TestClass]
    public class EntityFrameworkTest
    {
        [TestMethod]
        public void Get_list_all_articles_return()
        {
            DapperArticleDal articleDal = new DapperArticleDal();
            var result = articleDal.GetAll();
            //expected=> beklenen 
            Assert.AreEqual(1,result.Count);
        }

        [TestMethod]
        public void Get_All_with_parameter()
        {
            //DapperArticleDal articleDal = new DapperArticleDal();
            //var result = articleDal.GetAll(x => x.author.Contains("D"));
            //Assert.AreEqual(1,result.Count);
        }
    }
}
