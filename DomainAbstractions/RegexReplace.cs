using System.Text.RegularExpressions;
using ProgrammingParadigms;


namespace DomainAbstractions
{
    class RegexReplace : IDataFlow<string>
    {
        // This class can transforms a mathametical expression using a (highest precedence) binary or unary operator to one using a function
        // e.g. "3^2" => "Pow(3,2)"
        // It tries to do it on the cheap, i.e. not fully parsing the expression. So we make as few assumptions about the syntax of the expression as possible.
        // We assume the operator being replaced has equal highest precedence. 
        // if the operator is binary, we assume, for the moment, that it is right associative
        // if the operator is unary, we assume, for the moment, that it takes its operand from the left
        // We assume alphas, numbers, decimal point, curly braces and their content, '+' and '-' that are unary operators, and '+' & '-' that are part of a scientific number make up an operand.



        // Properties
        public string InstanceName { get; set; } = "Default";
        

        // Ports
        // The IDatFlow<string> implemented interface is the input
        private IDataFlow<string> output;



        public RegexReplace(string regex, string replace)
        {
            this.regex = new Regex(regex);
            this.replace = replace;
        }




        private Regex regex = null;
        private string replace = null;



        string IDataFlow<string>.Data { get => ""; set { output.Data = regex.Replace(value, replace); } }



    }
}
