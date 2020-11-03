using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
    public class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        readonly StringBuilder _resultStringBuilder;

        public ExpressionToFTSRequestTranslator()
        {
            _resultStringBuilder = new StringBuilder();
        }

        public string Translate(Expression exp)
        {
            Visit(exp);

            return _resultStringBuilder.ToString();
        }

        #region protected methods

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable)
                && node.Method.Name == "Where")
            {
                var predicate = node.Arguments[1];
                Visit(predicate);

                return node;
            }

            if (node.Method.Name == "StartsWith")
            {
                var constantNode = $"{((ConstantExpression)node.Arguments[0]).Value}*";

                var newNode = Expression.Constant(constantNode);
                Visit(node.Object);
                Visit(newNode);

                return node;
            }

            if (node.Method.Name == "Contains")
            {
                var constantNode = $"*{((ConstantExpression)node.Arguments[0]).Value}*";

                var newNode = Expression.Constant(constantNode);
                Visit(node.Object);
                Visit(newNode);

                return node;
            }

            if (node.Method.Name == "EndsWith")
            {
                var constantNode = $"*{((ConstantExpression)node.Arguments[0]).Value}";

                var newNode = Expression.Constant(constantNode);
                Visit(node.Object);
                Visit(newNode);

                return node;
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    var firstNode = node.Left.NodeType == ExpressionType.Constant ? node.Right : node.Left;
                    var secondNode = node.Right.NodeType == ExpressionType.Constant ? node.Right : node.Left;

                    Visit(firstNode);
                    Visit(secondNode);
                    break;

                case ExpressionType.AndAlso:
                    Visit(node.Left);
                    _resultStringBuilder.Append(",");
                    Visit(node.Right);
                    break;

                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            
            _resultStringBuilder.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _resultStringBuilder.Append("(");
            _resultStringBuilder.Append(node.Value);
            _resultStringBuilder.Append(")");

            return node;
        }

        #endregion
    }
}
