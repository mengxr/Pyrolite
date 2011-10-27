/* part of Pyrolite, by Irmen de Jong (irmen@razorvine.net) */

using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;

namespace Razorvine.Pyrolite.Pickle
{
	/// <summary>
	/// Exception thrown that represents a certain Python exception.
	/// </summary>
	public class PythonException : Exception, ISerializable
	{
		public String _pyroTraceback {get;set;}

		public PythonException()
		{
		}

	 	public PythonException(string message) : base(message)
		{
		}

		public PythonException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected PythonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>
		/// 'setstate' support for the unpickler to restore state
		/// </summary>
		public void setState(Hashtable values) {
			if(!values.ContainsKey("_pyroTraceback"))
				return;
			object tb=values["_pyroTraceback"];
			// if the traceback is a list of strings, create one string from it
			if(tb is ICollection) {
				StringBuilder sb=new StringBuilder();
				ICollection tbcoll=(ICollection)tb;
				foreach(object line in tbcoll) {
					sb.Append(line);
				}	
				_pyroTraceback=sb.ToString();
			} else {
				_pyroTraceback=(string)tb;
			}
			//Console.WriteLine("pythonexception state set to:{0}",_pyroTraceback);
		}

	}
}

