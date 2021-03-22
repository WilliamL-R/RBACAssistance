using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBACAssistance.Core.Objects
{
    public class UserList : IEnumerable
    {
        List<User> userList;

        public UserList()
        {
            this.userList = new List<User>();
        }

        public List<User> GetAsList()
        {
            return userList;
        }

        public void AddUserToList(User user)
        {
            if (!userList.Contains(user))
            {
                userList.Add(user);
            }
        }
        public void ClearList()
        {
            userList.Clear();
        }
        public IEnumerator GetEnumerator()
        {
            foreach (User val in userList)
            {
                yield return val;
            }
        }
        public User ElementAt(int index)
        {
            try
            {
                User item = userList.ElementAt<User>(index);
                return item;
            }
            catch (ArgumentOutOfRangeException e)
            {
                return null;
            }
        }

    }
}
