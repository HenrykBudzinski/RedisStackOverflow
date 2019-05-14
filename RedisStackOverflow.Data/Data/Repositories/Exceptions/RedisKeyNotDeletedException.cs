using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Data.Data.Repositories.Exceptions
{
    public class RedisKeyNotDeletedException : Exception
    {
        public RedisKeyNotDeletedException(string keyName)
            : base("Falha ao tentar deletar a key " + keyName + ".")
        {
        }
        public RedisKeyNotDeletedException(
            string keyName,
            Exception innerException)
            : base(
                "Falha ao tentar deletar a key " + keyName + ".",
                innerException)
        {

        }
        public RedisKeyNotDeletedException(params string[] keys)
            : base("Falha ao tentar deletar as keys " + string.Join(", ", keys) + ".")
        {
        }
        public RedisKeyNotDeletedException(
            Exception innerException,
            params string[] keys)
            : base(
                "Falha ao tentar deletar as keys " + string.Join(", ", keys) + ".",
                innerException)
        {

        }

    }
}
