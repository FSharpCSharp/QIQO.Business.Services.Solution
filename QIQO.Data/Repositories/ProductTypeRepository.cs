using QIQO.Common.Contracts;
using QIQO.Common.Core.Logging;
using QIQO.Data.Entities;
using QIQO.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QIQO.Data.Repositories
{
    public class ProductTypeRepository : RepositoryBase<ProductTypeData>, IProductTypeRepository
    {
        private IMainDBContext entity_context;

        public ProductTypeRepository(IMainDBContext dbc, IProductTypeMap map_factory) : base(map_factory)
        {
            entity_context = dbc;
        }

        public override IEnumerable<ProductTypeData> GetAll()
        {
            Log.Info("Accessing ProductTypeRepo GetAll function");
            using (entity_context)
            {
                DataSet ds = entity_context.ExecuteProcedureAsDataSet("usp_product_type_all");
                Log.Info("ProductTypeRepo ExecuteProcedureAsDataSet function call successful");
                return MapRows(ds);
            }
        }

        public IEnumerable<ProductTypeData> GetAllByCategory(string category)
        {
            Log.Info("Accessing ProductTypeRepo GetByID function");
            List<SqlParameter> pcol = new List<SqlParameter>() { new SqlParameter("@product_type_category", category) };
            using (entity_context)
            {
                DataSet ds = entity_context.ExecuteProcedureAsDataSet("usp_product_type_get_cat", pcol);
                Log.Info("ProductTypeRepo (GetByID) Passed ExecuteProcedureAsDataSet (usp_product_type_get_cat) function");
                return MapRows(ds);
            }
        }

        public override ProductTypeData GetByID(int product_type_key)
        {
            Log.Info("Accessing ProductTypeRepo GetByID function");
            List<SqlParameter> pcol = new List<SqlParameter>() { new SqlParameter("@product_type_key", product_type_key) };
            using (entity_context)
            {
                DataSet ds = entity_context.ExecuteProcedureAsDataSet("usp_product_type_get", pcol);
                Log.Info("ProductTypeRepo (GetByID) Passed ExecuteProcedureAsDataSet (usp_product_type_get) function");
                return MapRow(ds);
            }
        }

        public override ProductTypeData GetByCode(string product_type_code, string entity_code)
        {
            Log.Info("Accessing ProductTypeRepo GetByCode function");
            List<SqlParameter> pcol = new List<SqlParameter>() {
                new SqlParameter("@product_type_code", product_type_code),
                new SqlParameter("@company_code", entity_code)
            };
            using (entity_context)
            {
                DataSet ds = entity_context.ExecuteProcedureAsDataSet("usp_product_type_get_c", pcol);
                Log.Info("ProductTypeRepo (GetByCode) Passed ExecuteProcedureAsDataSet (usp_product_type_get_c) function");
                return MapRow(ds);
            }
        }

        public override int Insert(ProductTypeData entity)
        {
            Log.Info("Accessing ProductTypeRepo Insert function");
            if (entity != null)
                return Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override int Save(ProductTypeData entity)
        {
            Log.Info("Accessing ProductTypeRepo Save function");
            if (entity != null)
                return Upsert(entity);
            else
                throw new ArgumentException(nameof(entity));
        }

        public override void Delete(ProductTypeData entity)
        {
            Log.Info("Accessing ProductTypeRepo Delete function");
            using (entity_context)
            {
                entity_context.ExecuteProcedureNonQuery("usp_product_type_del", Mapper.MapParamsForDelete(entity));
            }
        }

        public override void DeleteByCode(string entity_code)
        {
            Log.Info("Accessing ProductTypeRepo DeleteByCode function");
            List<SqlParameter> pcol = new List<SqlParameter>() { new SqlParameter("@product_type_code", entity_code) };
            pcol.Add(Mapper.GetOutParam());
            using (entity_context)
            {
                entity_context.ExecuteProcedureNonQuery("usp_product_type_del_c", pcol);
            }
        }

        public override void DeleteByID(int entity_key)
        {
            Log.Info("Accessing ProductTypeRepo Delete function");
            using (entity_context)
            {
                entity_context.ExecuteProcedureNonQuery("usp_product_type_del", Mapper.MapParamsForDelete(entity_key));
            }
        }

        private int Upsert(ProductTypeData entity)
        {
            using (entity_context)
            {
                return entity_context.ExecuteProcedureNonQuery("usp_product_type_ups", Mapper.MapParamsForUpsert(entity));
            }
        }
    }
}