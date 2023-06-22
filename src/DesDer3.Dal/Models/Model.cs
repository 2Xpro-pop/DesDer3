using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesDer3.Dal.Models;

/// <summary>
/// The base abstract class for all models in the data access layer.
/// Provides common functionality and properties for models.
/// The GetHashCode() and Equals() methods are implemented in the Model class 
/// to provide a consistent and reliable way of comparing and identifying models.
/// </summary>
public abstract class Model
{
    public virtual Guid Id
    {
        get; set;
    }

    public Model()
    {
        Id = Guid.NewGuid();
    }

    public override int GetHashCode() => Id.GetHashCode();

    /// <summary>
    /// Determines whether the current model is equal to another object.
    /// Two models are considered equal if they have the same type and the same identifier.
    /// </summary>
    /// <param name="obj">The object to compare with the current model.</param>
    /// <returns>True if the models are equal; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if(obj is not Model model)
        {
            return false;
        }

        return model.Id.Equals(Id);
    }
}
