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
    public class DapperConferenceDal : IConferenceDal
    {

        private IDbConnection _iConnection;

        public DapperConferenceDal()
        {
            _iConnection = DapperDbContext.GetDbConnection();
        }

        public void Add(conference entity)
        {
            _iConnection.ExecuteScalar<conference>(
                "INSERT INTO conference (editor, booktitle, author, month, note, pages, title, volume, year, address," +
                " bibtexkey, entrytype, series, organization, publisher) VALUES( @editor, @booktitle, @author, @month, @note, @pages, @title," +
                " @volume, @year, @address, @bibtexkey, @entrytype, @series, @organization, @publisher)", new
                {
                    entity.entrytype,
                    entity.bibtexkey,
                    entity.editor,
                    entity.booktitle,
                    entity.author,
                    entity.month,
                    entity.note,
                    entity.pages,
                    entity.title,
                    entity.volume,
                    entity.year,
                    entity.series,
                    entity.address,
                    entity.organization,
                    entity.publisher,
                });
        }

        public int Count()
        {
            string query = @"SELECT COUNT(ID) FROM conference";
            int count = _iConnection.ExecuteScalar<int>(query);
            return count;
        }

        public void Delete(int ID)
        {
            string q = $"Delete from conference where ID = @ID";
            _iConnection.Execute(q,
                new
                {
                    ID
                });
        }
      
        public List<conference> GetAll(string filter = null)
        {
            List<conference> listvalues;
            string query = "Select * from conference";
            if (filter != null)
            {
                filter = filter.ToLower();
            }
            if (String.IsNullOrEmpty(filter) || filter == "*")
            {
                listvalues = _iConnection.Query<conference>(query).ToList();
                return listvalues;
            }

            if (filter.StartsWith("K. Oğuz") || filter.StartsWith("K. Oguz") || filter.StartsWith("K. oğuz") || filter.StartsWith("K. oguz") || filter.StartsWith("k. oguz") || filter.StartsWith("k. oğuz"))
            {
                filter = "Kaya Oğuz".ToLower();
                query += " Where (year like @value) or (month like @value) or (CAST(volume as INTEGER) LIKE @value) or (CAST(series as INTEGER) LIKE @value) or lower(author) || lower(title) || lower(bibtexkey) || lower(entrytype) || lower(booktitle) || lower(publisher) LIKE @value";
                listvalues = _iConnection.Query<conference>(query, new { value = '%' + filter + '%' }).ToList();
                return listvalues;
            }
            if (filter.Contains(".") && filter.Contains(".*"))
            {
                int index = filter.IndexOf(".*");
                filter = filter.Substring(0, index);
                query += " Where (year like @value) or (month like @value) or (CAST(volume as INTEGER) LIKE @value) or (CAST(series as INTEGER) LIKE @value) or lower(author) || lower(title) || lower(bibtexkey) || lower(entrytype) || lower(booktitle) || lower(publisher) LIKE @value";
                listvalues = _iConnection.Query<conference>(query, new { value = '%' + filter + '%' }).ToList();
                return listvalues;
            }
            query += " Where (year like @value) or (month like @value) or (CAST(volume as INTEGER) LIKE @value) or (CAST(series as INTEGER) LIKE @value) or lower(author) || lower(title) || lower(bibtexkey) || lower(entrytype) || lower(booktitle) || lower(publisher) LIKE @value";
            listvalues = _iConnection.Query<conference>(query, new { value = '%' + filter + '%' }).ToList();
            return listvalues;
        }

        public List<conference> GetAllByAuthorOrTitleIfNotExist(string author = null, string title = null)
        {
            string query = @"Select * from article";
            if (!String.IsNullOrEmpty(author) || !String.IsNullOrEmpty(title))
            {
                query += " Where ";
                if (!String.IsNullOrEmpty(author) && !String.IsNullOrEmpty(title))
                {
                    query += "author LIKE @value and title LIKE @value2";
                    return _iConnection.Query<conference>(query, new { value = "%" + author + "%", value2 = "%" + title + "%" }).ToList();
                }
                else if (!String.IsNullOrEmpty(author))
                {
                    query += "author LIKE @value";
                    return _iConnection.Query<conference>(query, new { value = "%" + author + "%" }).ToList();
                }
                else
                {
                    query += "title LIKE @value";
                    return _iConnection.Query<conference>(query, new { value = "%" + title + "%" }).ToList();
                }
            }
            else
            {
                return _iConnection.Query<conference>(query).ToList();
            }
        }

        public List<conference> GetAllByYear(int? year)
        {
            string query = @"Select * from article";
            if (year.HasValue)
            {
                query += "Where year = @year";
            }
            return _iConnection.Query<conference>(query, new { year }).ToList();
        }

        public conference GetByID(int ID)
        {
            return _iConnection.Query<conference>(
                  $"Select * from conference where ID = {ID}").FirstOrDefault();
        }

        public void Update(conference entity)
        {
            _iConnection.ExecuteScalar<conference>(
              "UPDATE conference SET author=@author, editor=@editor, booktitle=@booktitle, entrytype=@entrytype, bibtexkey=@bibtexkey," +
              "month=@month, note=@note, pages=@pages, series=@series, address=@address, organization=@organization," +
              "publisher=@publisher, title=@title, volume=@volume, year=@year where ID = @ID", new
              {
                  entity.editor,
                  entity.booktitle,
                  entity.entrytype,
                  entity.bibtexkey,
                  entity.author,
                  entity.month,
                  entity.note,
                  entity.pages,
                  entity.title,
                  entity.volume,
                  entity.year,
                  entity.ID,
                  entity.series,
                  entity.address,
                  entity.organization,
                  entity.publisher,
              });
        }
    }
}
