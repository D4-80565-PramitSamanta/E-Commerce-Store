using Core.Interfaces;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        private readonly Expression<Func<T, bool>> _criteria;

        protected BaseSpecification()
        {

        }

        protected BaseSpecification(Expression <Func<T,bool>> criteria)
        {
            this._criteria = criteria;
        }

        public Expression<Func<T, bool>>? Criteria => this._criteria;


        public Expression<Func<T, object>>? OrderBy { get; private set; }
        protected void AddOrderBy(Expression<Func<T, object>> exp)
        {
            this.OrderBy = exp;
        }


        public Expression<Func<T, object>>? OrderByDescending { get; private set; }
        protected void AddOrderByDescending(Expression<Func<T, object>> exp)
        {
            this.OrderByDescending = exp;
        }

        
    }


    public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T,TResult>
    {
        private readonly Expression<Func<T, bool>> _criteria;

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T,bool>> criteiria) : base(criteiria)
        {
            this._criteria= criteiria;
        }

        public Expression<Func<T, TResult>>? Select { get;private set; } 
        protected void AddSelect(Expression<Func<T, TResult>> term)
        {
            this.Select = term;
        }

        public bool IsDistinct { get; private set; }
        protected void ApplyDistinct()
        {
            this.IsDistinct = true;
        }
    }
}
