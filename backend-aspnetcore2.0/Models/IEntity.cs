using System;
namespace Woa.Models
{
    public interface IEntity
    {
        object ToContract();
    }
}
