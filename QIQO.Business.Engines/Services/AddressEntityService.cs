﻿using QIQO.Business.Contracts;
using QIQO.Business.Entities;
using QIQO.Data.Entities;
using System;

namespace QIQO.Business.Engines
{
    public class AddressEntityService : IAddressEntityService
    {
        public Address Map(AddressData address_data)
        {
            Address address = new Address()
            {
                AddressKey = address_data.AddressKey,
                AddressType = (QIQOAddressType)address_data.AddressTypeKey,
                EntityKey = address_data.EntityKey,
                AddressLine1 = address_data.AddressLine1,
                AddressLine2 = address_data.AddressLine2,
                AddressLine3 = address_data.AddressLine3,
                AddressLine4 = address_data.AddressLine4,
                AddressCity = address_data.AddressCity,
                AddressState = address_data.AddressStateProv,
                AddressPostalCode = address_data.AddressPostalCode,
                AddressCounty = address_data.AddressCounty,
                AddressCountry = address_data.AddressCountry,
                AddressActiveFlag = Convert.ToBoolean(address_data.AddressActiveFlg),
                AddressDefaultFlag = Convert.ToBoolean(address_data.AddressDefaultFlg),
                AddressNotes = address_data.AddressNotes,
                EntityType = (QIQOEntityType)address_data.EntityTypeKey,
                AddedUserID = address_data.AuditAddUserId,
                AddedDateTime = address_data.AuditAddDatetime,
                UpdateUserID = address_data.AuditUpdateUserId,
                UpdateDateTime = address_data.AuditUpdateDatetime
            };

            return address;
        }

        public AddressData Map(Address address)
        {
            AddressData address_data = new AddressData()
            {
                AddressKey = address.AddressKey,
                AddressTypeKey = (int)address.AddressType,
                EntityKey = address.EntityKey,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,
                AddressLine3 = address.AddressLine3,
                AddressLine4 = address.AddressLine4,
                AddressCity = address.AddressCity,
                AddressStateProv = address.AddressState,
                AddressPostalCode = address.AddressPostalCode,
                AddressCounty = address.AddressCounty,
                AddressCountry = address.AddressCountry,
                AddressActiveFlg = Convert.ToInt32(address.AddressActiveFlag),
                AddressDefaultFlg = Convert.ToInt32(address.AddressDefaultFlag),
                AddressNotes = address.AddressNotes,
                EntityTypeKey = (int)address.EntityType
            };

            return address_data;
        }

        public AddressType Map(AddressTypeData address_type_data)
        {
            AddressType AddressType = new AddressType()
            {
                AddressTypeKey = address_type_data.AddressTypeKey,
                AddressTypeCode = address_type_data.AddressTypeCode,
                AddressTypeName = address_type_data.AddressTypeName,
                AddressTypeDesc = address_type_data.AddressTypeDesc,
                AddedUserID = address_type_data.AuditAddUserId,
                AddedDateTime = address_type_data.AuditAddDatetime,
                UpdateUserID = address_type_data.AuditUpdateUserId,
                UpdateDateTime = address_type_data.AuditUpdateDatetime
            };

            return AddressType;
        }

        public AddressTypeData Map(AddressType address_type)
        {
            AddressTypeData AddressType_data = new AddressTypeData()
            {
                AddressTypeKey = address_type.AddressTypeKey,
                AddressTypeCode = address_type.AddressTypeCode,
                AddressTypeName = address_type.AddressTypeName,
                AddressTypeDesc = address_type.AddressTypeDesc
            };

            return AddressType_data;
        }
    }
}
