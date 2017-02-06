using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models.DNN
{
    public class NoteRepository : INoteRepository
    {
        public void Add(Note model)
        {
            throw new NotImplementedException();
        }

        public int DeleteNote(int id, string password)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll(int page)
        {
            throw new NotImplementedException();
        }

        public int GetCountAll()
        {
            throw new NotImplementedException();
        }

        public int GetCountBySearch(string searchField, string searchQuery)
        {
            throw new NotImplementedException();
        }

        public string GetFileNameById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetNewPhotos()
        {
            throw new NotImplementedException();
        }

        public Note GetNoteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetNoteSummaryByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetRecentPosts()
        {
            throw new NotImplementedException();
        }

        public List<Note> GetSearchAll(int page, string searchField, string searchQuery)
        {
            throw new NotImplementedException();
        }

        public void Pinned(int id)
        {
            throw new NotImplementedException();
        }

        public void ReplyNote(Note model)
        {
            throw new NotImplementedException();
        }

        public void UpdateDownCount(string fileName)
        {
            throw new NotImplementedException();
        }

        public void UpdateDownCountById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateNote(Note model)
        {
            throw new NotImplementedException();
        }
    }
}
