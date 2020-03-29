using System;

namespace TreeStore.Core.Provider
{
    public static class Option
    {
        public static Option<T> SomeIfNotNull<T>(T value) where T : class
            => value is null ? Option.None<T>() : Some(value);

        public static Option<T> Some<T>(T value) => new Option<T>(value, true);

        public static Option<T> None<T>() => new Option<T>(default, false);
    }

    public readonly struct Option<T> : IEquatable<Option<T>>
    {
        internal Option(T value, bool hasValue)
        {
            this.Value = value;
            this.hasValue = hasValue;
        }

        private readonly bool hasValue;

        public bool HasValue => this.hasValue;

        public T Value { get; }

        public void Deconstruct(out bool has, out T value)
        {
            has = this.hasValue;
            value = this.Value;
        }

        #region (T)

        public static explicit operator T(Option<T> thisOption) => thisOption.HasValue
            ? thisOption.Value
            : throw new InvalidCastException("Value not set");

        #endregion (T)

        #region (Option<T>)

        public static implicit operator Option<T>(T v) => Option.Some(v);

        #endregion (Option<T>)

        #region IEquatable<Option<T>>

        public bool Equals(Option<T> other)
        {
            if (this.HasValue && other.HasValue)
                return this.Value.Equals(other.Value);
            return false;
        }

        #endregion IEquatable<Option<T>>

        #region IfNone/IfSome

        public void IfSome(Action<T> some)
        {
            if (this.HasValue)
                some(this.Value);
        }

        public void IfNone(Action none)
        {
            if (!this.HasValue)
                none();
        }

        #endregion IfNone/IfSome

        #region Match

        public R Match<R>(Func<T, R> some, Func<R> none) => this.HasValue ? some(this.Value) : none();

        public void Match(Action<T> some, Action none)
        {
            if (this.HasValue)
                some(this.Value);
            else
                none();
        }

        #endregion Match

        #region (Try)GetValue(OrDefault)

        public bool TryGetValue(out T value)
        {
            value = this.Value;
            return this.HasValue;
        }

        public T GetValueOrDefault(T defaultValue) => this.HasValue ? this.Value : defaultValue;

        #endregion (Try)GetValue(OrDefault)
    }
}