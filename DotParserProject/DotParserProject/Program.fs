﻿// Learn more about F# at http://fsharp.net

open System.IO
open System
open Microsoft.FSharp.Text
open Microsoft.FSharp.Reflection

open Yard.Generators.RNGLR.Parser    
open Yard.Generators.RNGLR.AST

open DotParserProject.DotParser

let src = "..\\..\\..\\test_inputs\\test2.dot"

let tokens = 
    let lexbuf = Lexing.LexBuffer<_>.FromTextReader <| new System.IO.StreamReader(src)
    seq { while not lexbuf.IsPastEndOfStream do
              yield DotParserProject.DotLexer.tokenize lexbuf }

match buildAst tokens with
| Error (pos, token, msg, debugFuns, _) ->
    printfn "Error on position %d, token %A: %s" pos token msg
| Success (ast, errors) ->
    ast.PrintAst()
    let args = {
        tokenToRange = fun _ -> Unchecked.defaultof<_>, Unchecked.defaultof<_>
        zeroPosition = Unchecked.defaultof<_>
        clearAST = false
        filterEpsilons = false
    }
    let result:List<double> = translate args ast errors
    //defaultAstToDot ast @"..\..\astFromSeq.dot"
    defaultAstToDot ast @"..\..\astFromDot.dot"
    printfn "%A" result
    Console.ReadKey(true) |> ignore