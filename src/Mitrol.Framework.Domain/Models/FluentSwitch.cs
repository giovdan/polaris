namespace Mitrol.Framework.Domain.Models
{
    using System;

    public static class FluentSwitch
    {
        public static FluentSwitch<T> On<T>(T value)
            => new FluentSwitch<T>(value);

        public static FluentSwitch<T, TResult> On<T, TResult>(T value, TResult defaultResult = default)
            => new FluentSwitch<T, TResult>(value, defaultResult);
    }

    public class FluentSwitch<T>
    {
        internal bool AreEqual(T comparisonValue) => Equals(Value, comparisonValue);
        internal bool Unhandled(T _) => !Handled;
        internal bool Handled { get; set; }

        public T Value { get; private set; }

        public FluentSwitch(T value) => Value = value;
    }

    public class FluentSwitch<T, TResult> : FluentSwitch<T>
    {
        public TResult Result { get; internal set; }

        public FluentSwitch(T value, TResult @default)
            : base(value) => Result = @default;

        public FluentSwitch<T, TResult> Case(T comparisonValue, Func<T, TResult> func)
        {
            if (AreEqual(comparisonValue))
            {
                Handled = true;
                Result = func(Value);
            }
            return this;
        }

        public TResult End() => Result;
    }

    public static class FluentSwitchExtensions
    {
        #region < FluentSwitch<T> >

        public static FluentSwitch<T> Ignore<T>(this FluentSwitch<T> fs, T comparisonValue)
        {
            if (fs.Handled) return fs;

            if (fs.AreEqual(comparisonValue))
            {
                fs.Handled = true;
            }

            return fs;
        }

        public static FluentSwitch<T> Ignore<T>(this FluentSwitch<T> fs, Func<T, bool> equals)
        {
            if (fs.Handled) return fs;

            if (equals(fs.Value))
            {
                fs.Handled = true;
            }

            return fs;
        }

        public static void Default<T>(this FluentSwitch<T> fs, Action<T> action)
        {
            fs.Case(fs.Unhandled, action);
        }

        public static FluentSwitch<T> Case<T>(this FluentSwitch<T> fs
            , T comparisonValue, Action<T> action)
        {
            if (fs.Handled) return fs;

            if (fs.AreEqual(comparisonValue))
            {
                fs.Handled = true;
                action(fs.Value);
            }

            return fs;
        }

        public static FluentSwitch<T> Case<T>(this FluentSwitch<T> fs
            , Func<T, bool> equals, Action<T> action)
        {
            if (fs.Handled) return fs;

            if (equals(fs.Value))
            {
                fs.Handled = true;
                action(fs.Value);
            }

            return fs;
        }

        #endregion < FluentSwitch<T> >

        #region < FluentSwitch<T, TResult>

        public static FluentSwitch<T, TResult> Ignore<T, TResult>(this FluentSwitch<T, TResult> fs
            , T comparisonValue)
        {
            if (fs.Handled) return fs;

            if (fs.AreEqual(comparisonValue))
            {
                fs.Handled = true;
            }

            return fs;
        }

        public static FluentSwitch<T, TResult> Ignore<T, TResult>(this FluentSwitch<T, TResult> fs
            , Func<T, bool> equals)
        {
            if (fs.Handled) return fs;

            if (equals(fs.Value))
            {
                fs.Handled = true;
            }

            return fs;
        }

        public static TResult Default<T, TResult>(this FluentSwitch<T, TResult> fs
            , Action<T> action)
        {
            return fs.Case(fs.Unhandled, action).End();
        }

        public static TResult Default<T, TResult>(this FluentSwitch<T, TResult> fs
            , Func<T, TResult> func)
        {
            return fs.Case(fs.Unhandled, func).End();
        }

        public static FluentSwitch<T, TResult> Case<T, TResult>(this FluentSwitch<T, TResult> fs
            , Func<T, bool> equals, Func<T, TResult> func)
        {
            if (fs.Handled) return fs;

            if (equals(fs.Value))
            {
                fs.Handled = true;
                fs.Result = func(fs.Value);
            }
            
            return fs;
        }

        public static FluentSwitch<T, TResult> Case<T, TResult>(this FluentSwitch<T, TResult> fs
            , T comparisonValue, Action<T> action)
        {
            if (fs.Handled) return fs;

            if (fs.AreEqual(comparisonValue))
            {
                fs.Handled = true;
                action(fs.Value);
            }

            return fs;
        }

        public static FluentSwitch<T, TResult> Case<T, TResult>(this FluentSwitch<T, TResult> fs
            , Func<T, bool> equals, Action<T> action)
        {
            if (fs.Handled) return fs;

            if (equals(fs.Value))
            {
                fs.Handled = true;
                action(fs.Value);
            }

            return fs;
        }

        #endregion < FluentSwitch<T, TResult>
    }
}
