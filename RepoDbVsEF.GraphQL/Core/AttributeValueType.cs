namespace RepoDbVsEF.GraphQL.Core
{
    using HotChocolate.Types;
    using RepoDbVsEF.Application.Models;


    #region < ScalarGraphType implementation >
    //public class AttributeValueType : ScalarGraphType
    //{
    //    public override object ParseValue(object value)
    //    {
    //        if (value == null)
    //            return null;

    //        if (value is string inputString)
    //        {
    //            try
    //            {
    //                return JsonConvert.DeserializeObject<AttributeValueItem>(inputString);
    //            }
    //            catch
    //            {
    //                throw new FormatException($"Failed to parse {nameof(AttributeValueItem)} from input '{inputString}'. Input should be a string of three comma-separated floats in X Y Z order, ex. 1.0,2.0,3.0");
    //            }
    //        }

    //        return ThrowValueConversionError(value);
    //    }

    //    public override object Serialize(object value)
    //    {
    //        if (value == null)
    //            return null;

    //        if (value is AttributeValueItem attributeValueItem)
    //        {
    //            return JsonConvert.SerializeObject(attributeValueItem);
    //        }

    //        return ThrowSerializationError(value);
    //    }

    //    public override object ParseLiteral(GraphQLValue value)
    //    {
    //        if (value is GraphQLNullValue)
    //            return null;

    //        if (value is GraphQLStringValue stringValue)
    //            return ParseValue((string)stringValue.Value);

    //        return ThrowLiteralConversionError(value);
    //    }
    //}
    #endregion

    public class AttributeValueType: ObjectType<AttributeValueItem>
    {
        
    }
}
