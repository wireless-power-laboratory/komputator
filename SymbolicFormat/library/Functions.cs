namespace SymbolicFunctionLibrary {
	using System;
	using System.Collections;
	//using SymbolicFunctionLibrary;
	
	
	public class Functions {
		public static ArrayList GetFunctions( ) {
			ArrayList list = new ArrayList( );
			list.AddRange( new FunctionDescription[] {
										new FunctionDescription( "Power", 0, new PowerFunction( ) ),
										new FunctionDescription( "Times", 1, new TimesFunction( ) ),
										new FunctionDescription( "Divide", 1, new DivideFunction( ) ),
										new FunctionDescription( "Plus", 2, new PlusFunction( ) ),
										new FunctionDescription( "Minus", 2, new MinusFunction( ) )
										// insert new functions here
										} );
			return list;
		}
		
		public static ArrayList GetAllIndexesOf( string source, string val ) {
			ArrayList Indexes = new ArrayList( );
			int test = source.IndexOf( val, 0 );
			
			if ( test != -1 ) {
				Indexes.Insert( 0, test );
				int i=1;
				do  {
					Indexes.Insert( i,
						source.IndexOf( val, (int)Indexes[i-1]+1 )
						);
					i++;
				} while ( ((int)Indexes[i-1]) != -1 );
				Indexes.Remove( (-1) );
			}
			
			return Indexes;
		}
	}
	
	public class PowerFunction : IFunction {
		private ArrayList parameters;
		
#region SymbolicCalculator.IFunction interface implementation
		public int ResolveParameters( ArrayList nodes ) {
			parameters = new ArrayList( );
			
			parameters.Add( nodes[index-1] );
			parameters.Add( nodes[index+1] );
			
			nodes.RemoveAt( index-1 );
			nodes.RemoveAt( index );
			
			Index--; // removed an element 
			return 2;
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 0;
			}
		}
		
		public string FunctionDescription {
			get {
				return "Raise {0} to the {1} power";
			}
		}
		
		private int index;
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}
		
		public INode Evaluate( )
		{			
			// if we have functions or parens as params, 
			// evaluate them
			if ( ((INode)parameters[0]).Type == NodeType.Function )
				parameters[0] = ((IFunction)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[0]).Type == NodeType.ParenthesisBlock )
				parameters[0] = ((ParenthesisBlock)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.Function )
			    parameters[1] = ((IFunction)parameters[1]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.ParenthesisBlock )
				parameters[1] = ((ParenthesisBlock)parameters[1]).Evaluate( );
			
			return EvalBasedOnParamTypes( );
		}
		
		private INode RealReal( ) {
			// must return a real
			Real retReal = new Real( );
			retReal.Value = Math.Pow( ((Real)parameters[0]).Value,
							((Real)parameters[1]).Value );
			
			return retReal;
		}
		
		private INode EvalBasedOnParamTypes( ) {
			// now act based on what kind of params we have
			switch ( ((INode)parameters[0]).Type ) {
				case NodeType.Real:
					switch ( ((INode)parameters[1]).Type ) {
						case NodeType.Real:
							return RealReal( );
//						case NodeType.Variable:
//							return RealVariable( );
//						case NodeType.Imaginary:
//							return RealImaginary( );
//					}
//					break;
//				case NodeType.Variable:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return VariableReal( );
//						case NodeType.Variable:
//							return VariableVariable( );
//						case NodeType.Imaginary:
//							return VariableImaginary( );
//					}
//					break;
//				case NodeType.Imaginary:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return ImaginaryReal( );
//						case NodeType.Variable:
//							return ImaginaryVariable( );
//						case NodeType.Imaginary:
//							return ImaginaryImaginary( );
//					}
//					break;
					default:
						throw new Exception( "Parameter Error" );
				}
				default:
					throw new Exception( "Parameter Error" );
			}
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public NodeType Type {
			get {
				return NodeType.Function;
			}
		}
		
		public string Name {
			get {
				return "Power";
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "^" );
		}
#endregion

	}

	public class TimesFunction : IFunction {
		private ArrayList parameters;
		
#region SymbolicCalculator.IFunction interface implementation
		public int ResolveParameters( ArrayList nodes ) {
			parameters = new ArrayList( );
			
			parameters.Add( nodes[index-1] );
			parameters.Add( nodes[index+1] );
			
			nodes.RemoveAt( index-1 );
			nodes.RemoveAt( index );
			
			Index--; // removed an element 
			return 2;
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 1;
			}
		}
		
		public string FunctionDescription {
			get {
				return "Multiply {0} by {1}";
			}
		}
		
		private int index;
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}
		
		public INode Evaluate()
		{
			// if we have functions or parens as params, 
			// evaluate them
			if ( ((INode)parameters[0]).Type == NodeType.Function )
				parameters[0] = ((IFunction)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[0]).Type == NodeType.ParenthesisBlock )
				parameters[0] = ((ParenthesisBlock)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.Function )
			    parameters[1] = ((IFunction)parameters[1]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.ParenthesisBlock )
				parameters[1] = ((ParenthesisBlock)parameters[1]).Evaluate( );
			
			return EvalBasedOnParamTypes( );
		}
		
		private INode RealReal( ) {
			// must return a real
			Real retReal = new Real( );
			retReal.Value = ((Real)parameters[0]).Value *
							((Real)parameters[1]).Value;
			
			return retReal;
		}
		
		private INode EvalBasedOnParamTypes( ) {
			// now act based on what kind of params we have
			switch ( ((INode)parameters[0]).Type ) {
				case NodeType.Real:
					switch ( ((INode)parameters[1]).Type ) {
						case NodeType.Real:
							return RealReal( );
//						case NodeType.Variable:
//							return RealVariable( );
//						case NodeType.Imaginary:
//							return RealImaginary( );
//					}
//					break;
//				case NodeType.Variable:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return VariableReal( );
//						case NodeType.Variable:
//							return VariableVariable( );
//						case NodeType.Imaginary:
//							return VariableImaginary( );
//					}
//					break;
//				case NodeType.Imaginary:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return ImaginaryReal( );
//						case NodeType.Variable:
//							return ImaginaryVariable( );
//						case NodeType.Imaginary:
//							return ImaginaryImaginary( );
//					}
//					break;
					default:
						throw new Exception( "Parameter Error" );
				}
				default:
					throw new Exception( "Parameter Error" );
			}
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public NodeType Type {
			get {
				return NodeType.Function;
			}
		}
		
		public string Name {
			get {
				return "Times";
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "*" );
		}
#endregion

	}

	public class DivideFunction : IFunction {
		private ArrayList parameters;
		
#region SymbolicCalculator.IFunction interface implementation
		public int ResolveParameters( ArrayList nodes ) {
			parameters = new ArrayList( );
			
			parameters.Add( nodes[index-1] );
			parameters.Add( nodes[index+1] );
			
			nodes.RemoveAt( index-1 );
			nodes.RemoveAt( index );
			
			Index--; // removed an element 
			return 2;
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 1;
			}
		}
		
		public string FunctionDescription {
			get {
				return "Divide {0} by {1}";
			}
		}
		
		private int index;
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}
		
		public INode Evaluate( )
		{
			// if we have functions or parens as params, 
			// evaluate them
			if ( ((INode)parameters[0]).Type == NodeType.Function )
				parameters[0] = ((IFunction)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[0]).Type == NodeType.ParenthesisBlock )
				parameters[0] = ((ParenthesisBlock)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.Function )
			    parameters[1] = ((IFunction)parameters[1]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.ParenthesisBlock )
				parameters[1] = ((ParenthesisBlock)parameters[1]).Evaluate( );
			
			return EvalBasedOnParamTypes( );
		}
		
		private INode RealReal( ) {
			// must return a real
			Real retReal = new Real( );
			retReal.Value = ((Real)parameters[0]).Value /
							((Real)parameters[1]).Value;
			
			return retReal;
		}
		
		private INode EvalBasedOnParamTypes( ) {
			// now act based on what kind of params we have
			switch ( ((INode)parameters[0]).Type ) {
				case NodeType.Real:
					switch ( ((INode)parameters[1]).Type ) {
						case NodeType.Real:
							return RealReal( );
//						case NodeType.Variable:
//							return RealVariable( );
//						case NodeType.Imaginary:
//							return RealImaginary( );
//					}
//					break;
//				case NodeType.Variable:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return VariableReal( );
//						case NodeType.Variable:
//							return VariableVariable( );
//						case NodeType.Imaginary:
//							return VariableImaginary( );
//					}
//					break;
//				case NodeType.Imaginary:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return ImaginaryReal( );
//						case NodeType.Variable:
//							return ImaginaryVariable( );
//						case NodeType.Imaginary:
//							return ImaginaryImaginary( );
//					}
//					break;
					default:
						throw new Exception( "Parameter Error" );
				}
				default:
					throw new Exception( "Parameter Error" );
			}
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public NodeType Type {
			get {
				return NodeType.Function;
			}
		}
		
		public string Name {
			get {
				return "Divide";
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "/" );
		}
#endregion

	}

	public class PlusFunction : IFunction {
		private ArrayList parameters;
		
#region SymbolicCalculator.IFunction interface implementation
		public int ResolveParameters( ArrayList nodes ) {
			parameters = new ArrayList( );
			
			parameters.Add( nodes[index-1] );
			parameters.Add( nodes[index+1] );
			
			nodes.RemoveAt( index-1 );
			// one less element in the array now
			nodes.RemoveAt( index );
			
			Index--; // removed an element 
			return 2;
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 2;
			}
		}
		
		public string FunctionDescription {
			get {
				return "Add {0} to {1}";
			}
		}
		
		private int index;
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}
		
		public INode Evaluate( )
		{
			// if we have functions or parens as params, 
			// evaluate them
			if ( ((INode)parameters[0]).Type == NodeType.Function )
				parameters[0] = ((IFunction)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[0]).Type == NodeType.ParenthesisBlock )
				parameters[0] = ((ParenthesisBlock)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.Function )
			    parameters[1] = ((IFunction)parameters[1]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.ParenthesisBlock )
				parameters[1] = ((ParenthesisBlock)parameters[1]).Evaluate( );
			
			return EvalBasedOnParamTypes( );
		}
		
		private INode RealReal( ) {
			// must return a real
			Real retReal = new Real( );
			retReal.Value = ((Real)parameters[0]).Value +
							((Real)parameters[1]).Value;
			
			return retReal;
		}
		
		private INode EvalBasedOnParamTypes( ) {
			// now act based on what kind of params we have
			switch ( ((INode)parameters[0]).Type ) {
				case NodeType.Real:
					switch ( ((INode)parameters[1]).Type ) {
						case NodeType.Real:
							return RealReal( );
//						case NodeType.Variable:
//							return RealVariable( );
//						case NodeType.Imaginary:
//							return RealImaginary( );
//					}
//					break;
//				case NodeType.Variable:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return VariableReal( );
//						case NodeType.Variable:
//							return VariableVariable( );
//						case NodeType.Imaginary:
//							return VariableImaginary( );
//					}
//					break;
//				case NodeType.Imaginary:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return ImaginaryReal( );
//						case NodeType.Variable:
//							return ImaginaryVariable( );
//						case NodeType.Imaginary:
//							return ImaginaryImaginary( );
//					}
//					break;
					default:
						throw new Exception( "Parameter Error" );
				}
				default:
					throw new Exception( "Parameter Error" );
			}
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public NodeType Type {
			get {
				return NodeType.Function;
			}
		}
		
		public string Name {
			get {
				return "Plus";
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "+" );
		}
#endregion

	}

	public class MinusFunction : IFunction {
		private ArrayList parameters;
		
#region SymbolicCalculator.IFunction interface implementation
		public int ResolveParameters( ArrayList nodes ) {
			parameters = new ArrayList( );
			
			parameters.Add( nodes[index-1] );
			parameters.Add( nodes[index+1] );
			
			// remove the parameters we just registered
			nodes.RemoveAt( index-1 );
			nodes.RemoveAt( index );
						
			Index--; // removed an element 
			return 2;
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 2;
			}
		}
		
		public string FunctionDescription {
			get {
				return "Subtract {1} from {0}";
			}
		}
		
		private int index;
		public int Index {
			get {
				return index;
			}
			set {
				index = value;
			}
		}
		
		public INode Evaluate( )
		{
			// if we have functions or parens as params, 
			// evaluate them
			if ( ((INode)parameters[0]).Type == NodeType.Function )
				parameters[0] = ((IFunction)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[0]).Type == NodeType.ParenthesisBlock )
				parameters[0] = ((ParenthesisBlock)parameters[0]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.Function )
			    parameters[1] = ((IFunction)parameters[1]).Evaluate( );
			
			if ( ((INode)parameters[1]).Type == NodeType.ParenthesisBlock )
				parameters[1] = ((ParenthesisBlock)parameters[1]).Evaluate( );
			
			return EvalBasedOnParamTypes( );
		}
		
		private INode RealReal( ) {
			// must return a real
			Real retReal = new Real( );
			retReal.Value = ((Real)parameters[0]).Value -
							((Real)parameters[1]).Value;
			
			return retReal;
		}
		
		private INode EvalBasedOnParamTypes( ) {
			// now act based on what kind of params we have
			switch ( ((INode)parameters[0]).Type ) {
				case NodeType.Real:
					switch ( ((INode)parameters[1]).Type ) {
						case NodeType.Real:
							return RealReal( );
//						case NodeType.Variable:
//							return RealVariable( );
//						case NodeType.Imaginary:
//							return RealImaginary( );
//					}
//					break;
//				case NodeType.Variable:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return VariableReal( );
//						case NodeType.Variable:
//							return VariableVariable( );
//						case NodeType.Imaginary:
//							return VariableImaginary( );
//					}
//					break;
//				case NodeType.Imaginary:
//					switch ( ((INode)parameters[1]).Type ) {
//						case NodeType.Real:
//							return ImaginaryReal( );
//						case NodeType.Variable:
//							return ImaginaryVariable( );
//						case NodeType.Imaginary:
//							return ImaginaryImaginary( );
//					}
//					break;
					default:
						throw new Exception( "Parameter Error" );
				}
				default:
					throw new Exception( "Parameter Error" );
			}
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public NodeType Type {
			get {
				return NodeType.Function;
			}
		}
		
		public string Name {
			get {
				return "Minus";
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "-" );
		}
#endregion

	}
}
