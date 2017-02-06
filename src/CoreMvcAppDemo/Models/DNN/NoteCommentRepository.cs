using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMvcAppDemo.Models.DNN
{
    public class NoteCommentRepository : INoteCommentRepository
    {
        private string _connectionString;
        private SqlConnection con;

        public NoteCommentRepository(string connectionString)
        {
            _connectionString = connectionString;
            con = new SqlConnection(_connectionString);
        }
        public void AddNoteComment(INoteCommentRepository model)
        {
            throw new NotImplementedException();
        }

        public int DeleteNoteComment(int boardId, int id, string password)
        {
            throw new NotImplementedException();
        }

        public int GetCountBy(int boardId, int id, string password)
        {
            throw new NotImplementedException();
        }

        public List<NoteComment> GetNoteComments(int boardId)
        {
            throw new NotImplementedException();
        }

        public List<NoteComment> GetRecentComments()
        {
            throw new NotImplementedException();
        }
    }
}
