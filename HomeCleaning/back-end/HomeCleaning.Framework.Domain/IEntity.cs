using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Framework.Domain
{
	 public interface IEntity : IEntity<int>
	 {
	 }

	public interface IEntity<T>
	{
		[Key] T Id { get; }
	}

	public abstract class Entity<T> : IEntity<T>
	{
		int? _requestedHashCode;

		[Key]
		public T Id { get; protected set; }

		public bool IsTransient()
		{
			return Equals(Id, default(T));
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Entity<T>))
				return false;
			if (ReferenceEquals(this, obj))
				return true;
			if (GetType() != obj.GetType())
				return false;
			Entity<T> item = (Entity<T>) obj;
			if (item.IsTransient() || IsTransient())
				return false;
			return Equals(item.Id, Id);
		}

		public override int GetHashCode()
		{
			if (!IsTransient())
			{
				if (!_requestedHashCode.HasValue)
					_requestedHashCode = Id.GetHashCode() ^ 31;
				// XOR for random distribution. See:
				// https://docs.microsoft.com/archive/blogs/ericlippert/guidelines-and-rules-for-gethashcode
				return _requestedHashCode.Value;
			}

			return base.GetHashCode();
		}

		public static bool operator ==(Entity<T> left, Entity<T> right)
		{
			if (Object.Equals(left, null))
				return (Object.Equals(right, null));
			else
				return left.Equals(right);
		}

		public static bool operator !=(Entity<T> left, Entity<T> right)
		{
			return !(left == right);
		}
	}

	
	public interface IAggregateRoot
	{
	}
	
	public interface IGetAudit
	{
		DateTime CreatedOn { get; }
		DateTime ModifiedOn { get; }
	}

	public abstract class ValueObject
	{
		protected static bool EqualOperator(ValueObject left, ValueObject right)
		{
			if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
			{
				return false;
			}
			return ReferenceEquals(left, null) || left.Equals(right);
		}
		protected static bool NotEqualOperator(ValueObject left, ValueObject right)
		{
			return !(EqualOperator(left, right));
		}
		protected abstract IEnumerable<object> GetAtomicValues();
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}
			ValueObject other = (ValueObject)obj;
			IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
			IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
			while (thisValues.MoveNext() && otherValues.MoveNext())
			{
				if (ReferenceEquals(thisValues.Current, null) ^
				    ReferenceEquals(otherValues.Current, null))
				{
					return false;
				}
				if (thisValues.Current != null &&
				    !thisValues.Current.Equals(otherValues.Current))
				{
					return false;
				}
			}
			return !thisValues.MoveNext() && !otherValues.MoveNext();
		}
		public override int GetHashCode()
		{
			return GetAtomicValues()
				.Select(x => x != null ? x.GetHashCode() : 0)
				.Aggregate((x, y) => x ^ y);
		}
// Other utility methods
	}
}