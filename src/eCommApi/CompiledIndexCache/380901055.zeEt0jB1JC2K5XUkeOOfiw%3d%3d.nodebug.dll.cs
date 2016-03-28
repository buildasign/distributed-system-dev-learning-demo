using Raven.Abstractions;
using Raven.Database.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System;
using Raven.Database.Linq.PrivateExtensions;
using Lucene.Net.Documents;
using System.Globalization;
using System.Text.RegularExpressions;
using Raven.Database.Indexing;

public class Index_Auto_Carts_ByTokenIdSortByTokenId : Raven.Database.Linq.AbstractViewGenerator
{
	public Index_Auto_Carts_ByTokenIdSortByTokenId()
	{
		this.ViewText = @"from doc in docs.Carts
select new {
	TokenId = doc.TokenId
}";
		this.ForEntityNames.Add("Carts");
		this.AddMapDefinition(docs => 
			from doc in ((IEnumerable<dynamic>)docs)
			where string.Equals(doc["@metadata"]["Raven-Entity-Name"], "Carts", System.StringComparison.InvariantCultureIgnoreCase)
			select new {
				TokenId = doc.TokenId,
				__document_id = doc.__document_id
			});
		this.AddField("TokenId");
		this.AddField("__document_id");
		this.AddQueryParameterForMap("TokenId");
		this.AddQueryParameterForMap("__document_id");
		this.AddQueryParameterForReduce("TokenId");
		this.AddQueryParameterForReduce("__document_id");
	}
}
