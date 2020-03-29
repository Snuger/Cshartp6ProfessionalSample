using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSyncPro.Contract.IRepository;
using DataSyncPro.Contract.IService;
using DataSyncPro.Db.Entity;

namespace DataSyncPro.Services
{
    public class SourceTableService : ISourceTableService
    {
        private ISourceTableRepository _repository;
        public bool Add(SourceTable model)
        {
            bool result = false;
            _repository.Add(model, (item, err) =>
            {
                if (err == null)
                    result = true;
            });
            return result;
        }

        public bool Delete(int key)
        {
            bool _result = false;
            _repository.Delete(key, (teky, err) =>
            {
                if (err == null)
                    _result = true;
            });
            return _result;
        }

        public bool Delete(SourceTable model)
        {
            throw new NotImplementedException();
        }

        public SourceTable GetItem(int tkey)
        {           
            throw new NotImplementedException();
        }

        public List<SourceTable> GetItems()
        {
            throw new NotImplementedException();
        }

        public bool Update(SourceTable model, int tkey)
        {
            bool _result = false;
            _repository.Update(model, (item, err) =>
            {
                if (err == null)
                    _result = true;
            });
            return _result;
        }
    }
}
