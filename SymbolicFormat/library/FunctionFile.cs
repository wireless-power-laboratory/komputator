namespace RuntimeSymbolicCalculator {
	using System;
	using System.Collections;
	using SymbolicFunctionLibrary;
	
	
	public class Functions {
		public ArrayList GetFunctions( ) {
			ArrayList list = new ArrayList( );
			list.AddRange( new FunctionDescription[] {
										new FunctionDescription( "Plus", 2, new PlusFunction( ) )
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
#region SymbolicCalculator.IFunction interface implementation
		public ArrayList Parameters {
			get {
				return null;
			}
			set {
			}
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 0;
			}
		}
		
		public INode Evaluate()
		{
			return null;
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "Power";
			}
			set {
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "^" );
		}
#endregion

	}
}

	public class TimesFunction : IFunction {
#region SymbolicCalculator.IFunction interface implementation
		public ArrayList Parameters {
			get {
				return null;
			}
			set {
			}
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 1;
			}
		}
		
		public INode Evaluate()
		{
			return null;
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "Times";
			}
			set {
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "*" );
		}
#endregion

	}
}

	public class DivideFunction : IFunction {
#region SymbolicCalculator.IFunction interface implementation
		public ArrayList Parameters {
			get {
				return null;
			}
			set {
			}
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 1;
			}
		}
		
		public INode Evaluate()
		{
			return null;
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "Divide";
			}
			set {
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "/" );
		}
#endregion

	}
}

	public class PlusFunction : IFunction {
#region SymbolicCalculator.IFunction interface implementation
		public ArrayList Parameters {
			get {
				return null;
			}
			set {
			}
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 2;
			}
		}
		
		public INode Evaluate()
		{
			return null;
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "Plus";
			}
			set {
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "+" );
		}
#endregion

	}
}

	public class MinusFunction : IFunction {
#region SymbolicCalculator.IFunction interface implementation
		public ArrayList Parameters {
			get {
				return null;
			}
			set {
			}
		}
		
		public int OrderOfOperationsLevel {
			get {
				return 2;
			}
		}
		
		public INode Evaluate()
		{
			return null;
		}
#endregion


#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "Minus";
			}
			set {
			}
		}
		
		public ArrayList GetStartingIndexes(string source)
		{
			return Functions.GetAllIndexesOf( source, "-" );
		}
#endregion

	}
}

