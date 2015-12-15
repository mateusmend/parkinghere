using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingHere.BO.Interface
{
    public interface Ivalidator<T> where T : class
    {
        bool IsValidBase(T domain);
        bool ValidateInsert(T domain);
    }
}
