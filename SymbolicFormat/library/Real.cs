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
	/// Represents a real number in an expression.
	/// </summary>
	/// <remarks>
	/// 	created by - Adam Berger
	/// 	created on - 10/19/2003 3:28:06 PM
	/// </remarks>
	public class Real : INode {
		double realValue; // numeric value of real
		
		public Real( ) {
			
		}
		
		public double Value {
			get {
				return realValue;
			}
			set {
				realValue = value;
			}
		}
		
		public void GetValue( int index, string source ) {
			int i = index;
			
			while ( Char.IsDigit( source[i] ) ||
				     source[i] == '.' ) { // it is a number
				i++;
			}
			
			Value = Double.Parse( source.Substring( index, i-index ) );
		}

#region SymbolicCalculator.INode interface implementation
		public string Name {
			get {
				return "Real";
			}
		}
		
		public NodeType Type {
			get {
				return NodeType.Real;
			}
		}
		
		// TODO: this member really should be static
		public ArrayList GetStartingIndexes( string source )
		{
			ArrayList retIndexes = new ArrayList( );
			bool lastIndexWasNumber = false;
			
			for ( int i=0; i<source.Length; i++ ) {
				if ( Char.IsDigit( source[i] ) ||
				     source[i] == '.' ) { // it is a number
					
					if ( !lastIndexWasNumber ) {
						retIndexes.Add( i );
						lastIndexWasNumber = true;
					}
				}
				else { // its not a number
					lastIndexWasNumber = false;
				}
			}
			
			return retIndexes;
		}
#endregion

	}
}
