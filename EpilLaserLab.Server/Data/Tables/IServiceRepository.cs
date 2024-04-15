﻿using EpilLaserLab.Server.Models;

namespace EpilLaserLab.Server.Data.Tables
{
    public interface IServiceRepository
    {
        public IEnumerable<Service> GetAll();
        public Service? Get(int id);
        public bool Add(Service service);
        public bool Update(Service serviceOld, Service serviceNew);
        public bool Delete(Service service);
        public bool CheckForDuplication(Service service);
        public bool AccessDelete(Service service);
        public IQueryable<Service> GetQuerable();
    }
}
