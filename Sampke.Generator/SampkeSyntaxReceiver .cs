using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace Sampke.Generator
{
    public class SampkeSyntaxReceiver : ISyntaxReceiver
    {

        public List<FieldDeclarationSyntax> CandidateFields { get; } = new List<FieldDeclarationSyntax>();


        /// <summary>
        /// xxx感兴趣的接口列表
        /// </summary>
        //private readonly List<InterfaceDeclarationSyntax> interfaceSyntaxList = new List<InterfaceDeclarationSyntax>();



        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            //if (syntaxNode is InterfaceDeclarationSyntax syntax)
            //{
            //    this.interfaceSyntaxList.Add(syntax);
            //}
            

            // 将具有至少一个 Attribute 的任何字段作为候选
            if (syntaxNode is FieldDeclarationSyntax fieldDeclarationSyntax
                && fieldDeclarationSyntax.AttributeLists.Count > 0)
            {
                CandidateFields.Add(fieldDeclarationSyntax);
            }
        }
    }
}
