﻿using BiBuddy.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibuddy.Business.Abstract
{
    public interface IArticleService:IService<article>
    {
        List<article> GetListByYear(int year);
        List<article> GetListByMonth(int month);
        List<article> GetListByAuthorName(string name);

    }
}
