public class UnorderedTuple<T>
{
    public readonly T item1;
    public readonly T item2;

    public UnorderedTuple(T item1, T item2)
    {
        this.item1 = item1;
        this.item2 = item2;
    }

    public UnorderedTuple(OrderedTuple<T> o)
    {
        this.item1 = o.item1;
        this.item2 = o.item2;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        UnorderedTuple<T> other;
        try
        {
            other = obj as UnorderedTuple<T>;
        }
        catch
        {
            return false;
        }
        if (item1.Equals(other.item1) && item2.Equals(other.item2))
        {
            return true;
        }
        else if (item1.Equals(other.item2) && item2.Equals(other.item1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator ==(UnorderedTuple<T> t1, UnorderedTuple<T> t2)
    {
        return t1.Equals(t2);
    }

    public static bool operator !=(UnorderedTuple<T> t1, UnorderedTuple<T> t2)
    {
        return !t1.Equals(t2);
    }

    public override int GetHashCode()
    {
        int i1 = item1.GetHashCode();
        int i2 = item2.GetHashCode();
        return i1 * i1 + i2 * i2;
    }
}

public class OrderedTuple<T>
{
    public readonly T item1;
    public readonly T item2;

    public OrderedTuple(T item1, T item2)
    {
        this.item1 = item1;
        this.item2 = item2;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        OrderedTuple<T> other;
        try
        {
            other = obj as OrderedTuple<T>;
        }
        catch
        {
            return false;
        }
        if (item1.Equals(other.item1) && item2.Equals(other.item2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator ==(OrderedTuple<T> t1, OrderedTuple<T> t2)
    {
        return t1.Equals(t2);
    }

    public static bool operator !=(OrderedTuple<T> t1, OrderedTuple<T> t2)
    {
        return !t1.Equals(t2);
    }

    public override int GetHashCode()
    {
        int i1 = item1.GetHashCode();
        int i2 = item2.GetHashCode();
        return i1 * 100 + i2;
    }
}