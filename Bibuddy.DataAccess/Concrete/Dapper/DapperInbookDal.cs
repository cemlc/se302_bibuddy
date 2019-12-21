﻿using Bibuddy.DataAccess.Abstract;
using Bibuddy.DataAccess.DatabaseContext.Dapper;
using BiBuddy.Entities.Concrete;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bibuddy.DataAccess.Concrete.Dapper
{
    public class DapperInbookDal : IInbookDal
    {

        private IDbConnection _iConnection;

        public DapperInbookDal()
        {
            _iConnection = DapperDbContext.GetDbConnection();
        }

        public void Add(inbook entity)
        {
            _iConnection.ExecuteScalar<article>(
               "INSERT INTO article (chapter, author, month, note, title, volume, year, address, type" +
               " bibtexkey, entrytype ,publisher, series, edition) VALUES( @chapter, @author, @month, @note, @title," +
               " @volume, @year, @addess, @type, @bibtexkey, @entrytype, @publisher, @series, @edition)", new
               {
                   entity.entrytype,
                   entity.bibtexkey,
                   entity.author,
                   entity.address,
                   entity.chapter, 
                   entity.publisher,
                   entity.series,
                   entity.type,
                   entity.title,
                   entity.month,
                   entity.note,
                   entity.volume,
                   entity.edition,
                   entity.year
               });
        }

        public int Count()
        {
            string query = @"SELECT COUNT(ID) FROM inbook";
            int count = _iConnection.ExecuteScalar<int>(query);
            return count;
        }

        public void Delete(int ID)
        {
            string q = $"Delete from inbook where ID = @ID";
            _iConnection.Execute(q,
                new
                {
                    ID
                });
        }

        public inbook Get(string filter = null)
        {
            throw new NotImplementedException();
        }

        public List<inbook> GetAll(string filter = null)
        {
            if (filter != null)
            {
                filter = filter.ToLower();
            }
            string query = "Select * from inbook";
            List<inbook> listvalues = _iConnection.Query<inbook>(query).ToList();

            //if (String.IsNullOrEmpty(filter) || filter == "*")
            //{
                return listvalues;
            //}
        }

        public List<inbook> GetAllByAuthorOrTitleIfNotExist(string author = null, string title = null)
        {
            string query = @"Select * from inbook";
            if (!String.IsNullOrEmpty(author) || !String.IsNullOrEmpty(title))
            {
                query += " Where ";
                if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(title))
                {
                    query += "author LIKE @value and title LIKE @value2";
                    return _iConnection.Query<inbook>(query, new { value = "%" + author + "%", value2 = "%" + title + "%" }).ToList();
                }
                else if (!String.IsNullOrEmpty(author))
                {
                    query += "author LIKE @value";
                    return _iConnection.Query<inbook>(query, new { value = "%" + author + "%" }).ToList();
                }
                else
                {
                    query += "title LIKE @value";
                    return _iConnection.Query<inbook>(query, new { value = "%" + title + "%" }).ToList();
                }
            }
            else
            {
                return _iConnection.Query<inbook>(query).ToList();
            }
        }

        public List<inbook> GetAllByYear(int? year)
        {
            string query = @"Select * from inbook";
            if (year.HasValue)
            {
                query += "Where year = @year";
            }
            return _iConnection.Query<inbook>(query, new { year }).ToList();
        }

        public inbook GetByID(int ID)
        {
            return _iConnection.Query<inbook>(
                     $"Select * from inbook where ID = {ID}").FirstOrDefault();
        }

        public void Update(inbook entity)
        {
            _iConnection.ExecuteScalar<inbook>(
               "UPDATE inbook SET author=@author, entrytype=@entrytype, bibtexkey=@bibtexkey, " +
               " month=@month, note=@note, chapter@chapter, publisher=@publisher, series=@series, type=@type " +
               "title=@title, address=@address, edition=@edition, volume=@volume, year=@year where ID = @ID", new
               {
                   entity.entrytype,
                   entity.bibtexkey,
                   entity.author,
                   entity.address,
                   entity.chapter,
                   entity.publisher,
                   entity.series,
                   entity.type,
                   entity.title,
                   entity.month,
                   entity.note,
                   entity.volume,
                   entity.edition,
                   entity.year,
                   entity.ID
               });
        }
    }
}
