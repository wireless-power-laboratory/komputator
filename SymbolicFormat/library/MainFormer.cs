using System.Collections;
using SymbolicFunctionLibrary;

namespace SymbolicCalculator 
{
	class MainFormer
	{
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.Button button;
		
	
		// THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
		// DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
		void OnParse(object sender, System.EventArgs e)
		{
			// just make sure the text is enclosed in parenthesis
			string txt = textBox.Text;
			textBox.Text = "(" + txt + ")";
			
			ArrayList functions = GetFuncDescs( );
			Parser p = new Parser( );
			INode topNode = p.Parse( textBox.Text, functions );
			
			INode resultNode = new Real( ); // have to initialize it to something
			
			switch ( topNode.Type ) {
				case NodeType.Function:
					IFunction func = (IFunction)topNode;
					resultNode = (INode)func.Evaluate( );
					break;
				case NodeType.ParenthesisBlock:
					ParenthesisBlock pBlock = (ParenthesisBlock)topNode;
					resultNode = (INode)pBlock.Evaluate( );
					break;
				case NodeType.Real:
					resultNode = topNode;
					break;
			}
			
			if ( resultNode.Type == NodeType.Real ) {
				Real r = (Real)resultNode;
				textBox.Text += "\r\n\r\n" + r.Value.ToString( );
			}
		}
		
		ArrayList GetFuncDescs( ) {
			// get every dll in specified dir and cycle for each one
			
			return Functions.GetFunctions( );
		}
		
		
			
		
	}			
}
