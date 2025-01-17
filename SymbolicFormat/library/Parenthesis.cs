//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace SymbolicFunctionLibrary {
	using System;
	using System.Collections;
	using SymbolicFunctionLibrary;
	
	
	/// <summary>
	/// Represents an opening bracket or parenthesis in an expression.
	/// </summary>
	/// <remarks>
	/// 	created by - Adam Berger
	/// 	created on - 10/19/2003 3:28:06 PM
	/// </remarks>
	public class OpenParenthesis : INode {
		int startingIndex;
		
		public OpenParenthesis( ) {
			
		}
		
		public int StartingIndex {
			get {
				return startingIndex;
			}
			set {
				startingIndex = value;
			}
		}

#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "OpenParenthesis";
			}
		}
		
		public NodeType Type {
			get {
				return NodeType.OpenParenthesis;
			}
		}
		
		// TODO: this member really should be static
		public ArrayList GetStartingIndexes( string source )
		{
			ArrayList retIndexes = new ArrayList( );
			
			for ( int i=0; i<source.Length; i++ ) {
				if ( source[i] == '(' ||
				     source[i] == '[' ||
				     source[i] == '{' ) {
					retIndexes.Add( i );    	
				}
			}
			
			return retIndexes;
		}
#endregion

	}
	
	/// <summary>
	/// Represents a closing bracket or parenthesis in an expression.
	/// </summary>
	/// <remarks>
	/// 	created by - Adam Berger
	/// 	created on - 10/19/2003 3:28:06 PM
	/// </remarks>
	public class CloseParenthesis : INode {
		int startingIndex;
		
		public CloseParenthesis( ) {
			
		}
		
		public int StartingIndex {
			get {
				return startingIndex;
			}
			set {
				startingIndex = value;
			}
		}

#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "CloseParenthesis";
			}
		}
		
		public NodeType Type {
			get {
				return NodeType.CloseParenthesis;
			}
		}
		
		// TODO: this member really should be static
		public ArrayList GetStartingIndexes( string source )
		{
			ArrayList retIndexes = new ArrayList( );
			
			for ( int i=0; i<source.Length; i++ ) {
				if ( source[i] == ')' ||
				     source[i] == ']' ||
				     source[i] == '}' ) {
					retIndexes.Add( i );    	
				}
			}
			
			return retIndexes;
		}
#endregion

	}
	
	/// <summary>
	/// Represents a parenthesis block in an expression.  e.x.: (...)
	/// </summary>
	/// <remarks>
	/// 	created by - Adam Berger
	/// 	created on - 10/19/2003 3:28:06 PM
	/// </remarks>
	public class ParenthesisBlock : INode {
		ArrayList contents;
		
		public ParenthesisBlock( ) {
			contents = new ArrayList( );
		}
		
		public ParenthesisBlock( ref ArrayList nodes ) {
			contents = new ArrayList( );
			ParseNodes( ref nodes );
		}
		
		private void ParseNodes( ref ArrayList nodes ) {
			// loop through and get everything between (...)
			SortedList openParens = new SortedList( );
			foreach ( INode node in nodes ) {
				if ( node.Type == NodeType.OpenParenthesis ) {
					openParens.Add( ((OpenParenthesis)node).StartingIndex, 
					               node 
					               );
				}
			}
			
			IList oList = openParens.GetKeyList( );
			
			// must be open parenthesis to have a parenthesis block
			if ( oList.Count < 1 )
				return;
			
			int index = (int)oList[openParens.Count-1] + 1;
			while ( ((INode)nodes[index]).Type != NodeType.CloseParenthesis ) {
				contents.Add( nodes[index] );
				// index++; we're removing an element each time so index stays the same
				nodes.RemoveAt( index );
			}
			
			// remove close and open parens
			nodes[--index] = this;
			nodes.RemoveAt( index + 1); // ( and ) should be next to each other now
			
			// put our Paren Block in
			//nodes.Insert( index, this );
		}
		
		public INode Evaluate( ) {
			int highestOrderOfOpsLevel = 2;
			
			// get all functions
			ArrayList funcs = new ArrayList( );
			for ( int i=0; i<contents.Count; i++ ) {
				INode n = (INode)contents[i];
				if ( n.Type == NodeType.Function ) {
					IFunction f = (IFunction)n;
					f.Index = i;
					funcs.Add( f );
				}
			}
			
			// loop through and let each function get its parameters
			for ( int i=0; i<=highestOrderOfOpsLevel; i++ ) {
				int nodeIndex = 0;
				for ( int j=0; j<funcs.Count; j++ ) {
					IFunction func = (IFunction)funcs[j];
					if ( func.OrderOfOperationsLevel == i ) {
						int nodesRemoved = func.ResolveParameters( contents );
						// change the indecies of the other functions after the
						// one removed
						foreach ( IFunction f in funcs ) {
							if ( f.Index > func.Index )
								f.Index -= nodesRemoved;
						}
					}
					
					nodeIndex++;
				}
			}
			
			// All that's left in contents is the topmost Node,
			// because that isn't a parameter for anything else
			INode topNode = (INode)contents[0];
			INode resultNode = topNode; // have to init to something
			
			// evaluate the top node
			switch ( topNode.Type ) {
				case NodeType.Function:
					IFunction func = (IFunction)topNode;
					resultNode = func.Evaluate( );
					break;
				case NodeType.ParenthesisBlock:
					ParenthesisBlock pBlock = (ParenthesisBlock)topNode;
					resultNode = pBlock.Evaluate( );
					break;
				case NodeType.Real:
					resultNode = topNode;
					break;
			}
			
			return resultNode;
		}
		
		public ArrayList Contents {
			get {
				return contents;
			}
			set {
				contents = value;
			}
		}

#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "ParenthesisBlock";
			}
		}
		
		public NodeType Type {
			get {
				return NodeType.ParenthesisBlock;
			}
		}
		
		// TODO: this member really should be static
		public ArrayList GetStartingIndexes( string source )
		{
			// not really supported or needed here
			ArrayList retIndexes = new ArrayList( );
			
			return retIndexes;
		}
#endregion

	}	
}
