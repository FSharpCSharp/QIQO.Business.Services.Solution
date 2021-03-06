using QIQO.Common.Contracts;
using QIQO.Common.Core.Logging;
using QIQO.Data.Entities;
using QIQO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QIQO.Data.Repositories
{
    public class EntityTypeRepository : RepositoryBase<EntityTypeData>, IEntityTypeRepository
    {
        private IMainDBContext entity_context;
        
        public EntityTypeRepository(IMainDBContext dbc, IEntityTypeMap map_factory) : base(map_factory)
        {
            entity_context = dbc;
        }

        public override IEnumerable<EntityTypeData> GetAll()
        {
            Log.Info("Accessing EntityTypeRepo GetAll function");
            using (entity_context)
            {
                return MapRows(entity_context.ExecuteProcedureAsSqlDataReader("usp_entity_type_all"));
            }
        }

        public override EntityTypeData GetByID(int entity_type_key)
        {
            Log.Info("Accessing EntityTypeRepo GetByID function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@entity_type_key", entity_type_key) };
            using (entity_context)
            {
                return MapRow(entity_context.ExecuteProcedureAsSqlDataReader("usp_entity_type_get", pcol));
            }
        }

        public override EntityTypeData GetByCode(string entity_type_code, string entity_code)
        {
            Log.Info("Accessing EntityTypeRepo GetByCode function");
            var pcol = new List<SqlParameter>() {
                Mapper.BuildParam("@entity_type_code", entity_type_code),
                Mapper.BuildParam("@company_code", entity_code)
            };
            using (entity_context)
            {
                return MapRow(entity_context.ExecuteProcedureAsSqlDataReader("usp_entity_type_get_c", pcol));
            }
        }

        public override int Insert(EntityTypeData entity)
        {
            Log.Info("Accessing EntityTypeRepo Insert function");
            if (entity != null)
                return Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override int Save(EntityTypeData entity)
        {
            Log.Info("Accessing EntityTypeRepo Save function");
            if (entity != null)
                return Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(EntityTypeData entity)
        {
            Log.Info("Accessing EntityTypeRepo Delete function");
            using (entity_context)
            {
                entity_context.ExecuteProcedureNonQuery("usp_entity_type_del", Mapper.MapParamsForDelete(entity));
            }
        }

        public override void DeleteByCode(string entity_code)
        {
            Log.Info("Accessing EntityTypeRepo DeleteByCode function");
            var pcol = new List<SqlParameter>() { Mapper.BuildParam("@entity_type_code", entity_code) };
            pcol.Add(Mapper.GetOutParam());
            using (entity_context)
            {
                entity_context.ExecuteProcedureNonQuery("usp_entity_type_del_c", pcol);
            }
        }

        public override void DeleteByID(int entity_key)
        {
            Log.Info("Accessing EntityTypeRepo Delete function");
            using (entity_context)
            {
                entity_context.ExecuteProcedureNonQuery("usp_entity_type_del", Mapper.MapParamsForDelete(entity_key));
            }
        }

        private int Upsert(EntityTypeData entity)
        {
            using (entity_context)
            {
                return entity_context.ExecuteProcedureNonQuery("usp_entity_type_ups", Mapper.MapParamsForUpsert(entity));
            }
        }
    }
}