using DataObjectLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayerInterfaces
{
    /// <summary>
    /// Creator: Mohamed Elamin
    /// Created: 2025/04/15
    /// Approver: 
    /// Class for the Address Accessor Interface. 
    /// </summary>
    public interface IAddressAccessor
    {
        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="addressId"></param>
        /// <returns>adderss</returns>
        Address SelectAddressByAddressId(int addressId);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <returns>all adderss types </returns>
        List<string> SelectAllAddressTypes();

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="address"></param>

        int InsertAddress(Address address);

        /// <summary>
        /// Creator: Mohamed Elamin
        /// Created: 2025/04/15
        /// Approver:
        /// Actual summary of the method
        /// </summary>
        /// <remarks>
        /// Updater: Name
        /// Updated: yyyy/mm/dd 
        /// Update: (example: Fixed a problem when user inputs bad data)
        /// </remarks>
        /// <param name="oldAddress"></param>
        /// <param name="newAddress"></param>
        /// <returns>True or false depending if the record was edited </returns>
        int UpdateAddress(Address oldAddress, Address newAddress);
    }
}
