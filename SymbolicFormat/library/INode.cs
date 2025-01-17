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
	
	
	public enum NodeType {
			Real, Variable, Imaginary, Function, OpenParenthesis,
			CloseParenthesis, ParenthesisBlock, Term, Polynomial,
			Equation, Inequality };
	
	/// <summary>
	/// Represents a node of an expression.  This serves as the base class for functions, reals, imaginaries, variables, equations, inequalities, commas, parenthesis, etc.
	/// </summary>
	/// <remarks>
	/// 	created by - Adam Berger
	/// 	created on - 10/19/2003 3:19:55 PM
	/// </remarks>
	public interface INode {
		NodeType Type {
			get;
		}
		
		string Name {
			get;
		}
		
		ArrayList GetStartingIndexes( string source );
	}
}