// Implementation file for parser generated by fsyacc
module Parser
#nowarn "64";; // turn off warnings that type variables used in production annotations are instantiated to concrete type
open Microsoft.FSharp.Text.Lexing
open Microsoft.FSharp.Text.Parsing.ParseHelpers
# 1 "Parser.fsy"

open System
open Ast
open Microsoft.FSharp.Collections

exception EmptyFile
exception NoScope
exception NoPathConstraints

let addScope s (ds, ps, cs) = 
    {Name=s;
    Defs=ds;
    PConstraints=ps;
    CConstraints=cs}

let loc l = 
    if l = "in" then Ast.Inside 
    else if l = "out" then Ast.Outside 
    else Ast.Loc l

# 27 "Parser.fs"
// This type is the type of tokens accepted by the parser
type token = 
  | DEFINE
  | SCOPE
  | OWNS
  | PATHS
  | ORDERED
  | UNORDERED
  | AGGREGATE
  | COMMUNITY
  | MAXROUTES
  | MULTIPATH
  | INCLUDE
  | INTERSECT
  | UNION
  | NOT
  | STAR
  | SHR
  | AND
  | OR
  | TRUE
  | FALSE
  | EQUAL
  | LPAREN
  | RPAREN
  | LBRACKET
  | RBRACKET
  | LBRACE
  | RBRACE
  | COMMA
  | SLASH
  | COLON
  | SEMICOLON
  | RARROW
  | ROCKET
  | DOT
  | EOF
  | INT of (System.Int32)
  | ID of (string)
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_DEFINE
    | TOKEN_SCOPE
    | TOKEN_OWNS
    | TOKEN_PATHS
    | TOKEN_ORDERED
    | TOKEN_UNORDERED
    | TOKEN_AGGREGATE
    | TOKEN_COMMUNITY
    | TOKEN_MAXROUTES
    | TOKEN_MULTIPATH
    | TOKEN_INCLUDE
    | TOKEN_INTERSECT
    | TOKEN_UNION
    | TOKEN_NOT
    | TOKEN_STAR
    | TOKEN_SHR
    | TOKEN_AND
    | TOKEN_OR
    | TOKEN_TRUE
    | TOKEN_FALSE
    | TOKEN_EQUAL
    | TOKEN_LPAREN
    | TOKEN_RPAREN
    | TOKEN_LBRACKET
    | TOKEN_RBRACKET
    | TOKEN_LBRACE
    | TOKEN_RBRACE
    | TOKEN_COMMA
    | TOKEN_SLASH
    | TOKEN_COLON
    | TOKEN_SEMICOLON
    | TOKEN_RARROW
    | TOKEN_ROCKET
    | TOKEN_DOT
    | TOKEN_EOF
    | TOKEN_INT
    | TOKEN_ID
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
    | NONTERM_scope
    | NONTERM_cconstrs
    | NONTERM_cconstr
    | NONTERM_pconstrs
    | NONTERM_pconstr
    | NONTERM_regexs
    | NONTERM_regex
    | NONTERM_predicate
    | NONTERM_definitions
    | NONTERM_definition

// This function maps tokens to integer indexes
let tagOfToken (t:token) = 
  match t with
  | DEFINE  -> 0 
  | SCOPE  -> 1 
  | OWNS  -> 2 
  | PATHS  -> 3 
  | ORDERED  -> 4 
  | UNORDERED  -> 5 
  | AGGREGATE  -> 6 
  | COMMUNITY  -> 7 
  | MAXROUTES  -> 8 
  | MULTIPATH  -> 9 
  | INCLUDE  -> 10 
  | INTERSECT  -> 11 
  | UNION  -> 12 
  | NOT  -> 13 
  | STAR  -> 14 
  | SHR  -> 15 
  | AND  -> 16 
  | OR  -> 17 
  | TRUE  -> 18 
  | FALSE  -> 19 
  | EQUAL  -> 20 
  | LPAREN  -> 21 
  | RPAREN  -> 22 
  | LBRACKET  -> 23 
  | RBRACKET  -> 24 
  | LBRACE  -> 25 
  | RBRACE  -> 26 
  | COMMA  -> 27 
  | SLASH  -> 28 
  | COLON  -> 29 
  | SEMICOLON  -> 30 
  | RARROW  -> 31 
  | ROCKET  -> 32 
  | DOT  -> 33 
  | EOF  -> 34 
  | INT _ -> 35 
  | ID _ -> 36 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_DEFINE 
  | 1 -> TOKEN_SCOPE 
  | 2 -> TOKEN_OWNS 
  | 3 -> TOKEN_PATHS 
  | 4 -> TOKEN_ORDERED 
  | 5 -> TOKEN_UNORDERED 
  | 6 -> TOKEN_AGGREGATE 
  | 7 -> TOKEN_COMMUNITY 
  | 8 -> TOKEN_MAXROUTES 
  | 9 -> TOKEN_MULTIPATH 
  | 10 -> TOKEN_INCLUDE 
  | 11 -> TOKEN_INTERSECT 
  | 12 -> TOKEN_UNION 
  | 13 -> TOKEN_NOT 
  | 14 -> TOKEN_STAR 
  | 15 -> TOKEN_SHR 
  | 16 -> TOKEN_AND 
  | 17 -> TOKEN_OR 
  | 18 -> TOKEN_TRUE 
  | 19 -> TOKEN_FALSE 
  | 20 -> TOKEN_EQUAL 
  | 21 -> TOKEN_LPAREN 
  | 22 -> TOKEN_RPAREN 
  | 23 -> TOKEN_LBRACKET 
  | 24 -> TOKEN_RBRACKET 
  | 25 -> TOKEN_LBRACE 
  | 26 -> TOKEN_RBRACE 
  | 27 -> TOKEN_COMMA 
  | 28 -> TOKEN_SLASH 
  | 29 -> TOKEN_COLON 
  | 30 -> TOKEN_SEMICOLON 
  | 31 -> TOKEN_RARROW 
  | 32 -> TOKEN_ROCKET 
  | 33 -> TOKEN_DOT 
  | 34 -> TOKEN_EOF 
  | 35 -> TOKEN_INT 
  | 36 -> TOKEN_ID 
  | 39 -> TOKEN_end_of_input
  | 37 -> TOKEN_error
  | _ -> failwith "tokenTagToTokenId: bad token"

/// This function maps production indexes returned in syntax errors to strings representing the non terminal that would be produced by that production
let prodIdxToNonTerminal (prodIdx:int) = 
  match prodIdx with
    | 0 -> NONTERM__startstart 
    | 1 -> NONTERM_start 
    | 2 -> NONTERM_start 
    | 3 -> NONTERM_scope 
    | 4 -> NONTERM_scope 
    | 5 -> NONTERM_scope 
    | 6 -> NONTERM_scope 
    | 7 -> NONTERM_cconstrs 
    | 8 -> NONTERM_cconstrs 
    | 9 -> NONTERM_cconstr 
    | 10 -> NONTERM_cconstr 
    | 11 -> NONTERM_cconstr 
    | 12 -> NONTERM_cconstr 
    | 13 -> NONTERM_cconstr 
    | 14 -> NONTERM_pconstrs 
    | 15 -> NONTERM_pconstrs 
    | 16 -> NONTERM_pconstr 
    | 17 -> NONTERM_regexs 
    | 18 -> NONTERM_regexs 
    | 19 -> NONTERM_regex 
    | 20 -> NONTERM_regex 
    | 21 -> NONTERM_regex 
    | 22 -> NONTERM_regex 
    | 23 -> NONTERM_regex 
    | 24 -> NONTERM_regex 
    | 25 -> NONTERM_regex 
    | 26 -> NONTERM_regex 
    | 27 -> NONTERM_predicate 
    | 28 -> NONTERM_predicate 
    | 29 -> NONTERM_predicate 
    | 30 -> NONTERM_predicate 
    | 31 -> NONTERM_predicate 
    | 32 -> NONTERM_predicate 
    | 33 -> NONTERM_predicate 
    | 34 -> NONTERM_predicate 
    | 35 -> NONTERM_definitions 
    | 36 -> NONTERM_definitions 
    | 37 -> NONTERM_definition 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 39 
let _fsyacc_tagOfErrorTerminal = 37

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | DEFINE  -> "DEFINE" 
  | SCOPE  -> "SCOPE" 
  | OWNS  -> "OWNS" 
  | PATHS  -> "PATHS" 
  | ORDERED  -> "ORDERED" 
  | UNORDERED  -> "UNORDERED" 
  | AGGREGATE  -> "AGGREGATE" 
  | COMMUNITY  -> "COMMUNITY" 
  | MAXROUTES  -> "MAXROUTES" 
  | MULTIPATH  -> "MULTIPATH" 
  | INCLUDE  -> "INCLUDE" 
  | INTERSECT  -> "INTERSECT" 
  | UNION  -> "UNION" 
  | NOT  -> "NOT" 
  | STAR  -> "STAR" 
  | SHR  -> "SHR" 
  | AND  -> "AND" 
  | OR  -> "OR" 
  | TRUE  -> "TRUE" 
  | FALSE  -> "FALSE" 
  | EQUAL  -> "EQUAL" 
  | LPAREN  -> "LPAREN" 
  | RPAREN  -> "RPAREN" 
  | LBRACKET  -> "LBRACKET" 
  | RBRACKET  -> "RBRACKET" 
  | LBRACE  -> "LBRACE" 
  | RBRACE  -> "RBRACE" 
  | COMMA  -> "COMMA" 
  | SLASH  -> "SLASH" 
  | COLON  -> "COLON" 
  | SEMICOLON  -> "SEMICOLON" 
  | RARROW  -> "RARROW" 
  | ROCKET  -> "ROCKET" 
  | DOT  -> "DOT" 
  | EOF  -> "EOF" 
  | INT _ -> "INT" 
  | ID _ -> "ID" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | DEFINE  -> (null : System.Object) 
  | SCOPE  -> (null : System.Object) 
  | OWNS  -> (null : System.Object) 
  | PATHS  -> (null : System.Object) 
  | ORDERED  -> (null : System.Object) 
  | UNORDERED  -> (null : System.Object) 
  | AGGREGATE  -> (null : System.Object) 
  | COMMUNITY  -> (null : System.Object) 
  | MAXROUTES  -> (null : System.Object) 
  | MULTIPATH  -> (null : System.Object) 
  | INCLUDE  -> (null : System.Object) 
  | INTERSECT  -> (null : System.Object) 
  | UNION  -> (null : System.Object) 
  | NOT  -> (null : System.Object) 
  | STAR  -> (null : System.Object) 
  | SHR  -> (null : System.Object) 
  | AND  -> (null : System.Object) 
  | OR  -> (null : System.Object) 
  | TRUE  -> (null : System.Object) 
  | FALSE  -> (null : System.Object) 
  | EQUAL  -> (null : System.Object) 
  | LPAREN  -> (null : System.Object) 
  | RPAREN  -> (null : System.Object) 
  | LBRACKET  -> (null : System.Object) 
  | RBRACKET  -> (null : System.Object) 
  | LBRACE  -> (null : System.Object) 
  | RBRACE  -> (null : System.Object) 
  | COMMA  -> (null : System.Object) 
  | SLASH  -> (null : System.Object) 
  | COLON  -> (null : System.Object) 
  | SEMICOLON  -> (null : System.Object) 
  | RARROW  -> (null : System.Object) 
  | ROCKET  -> (null : System.Object) 
  | DOT  -> (null : System.Object) 
  | EOF  -> (null : System.Object) 
  | INT _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | ID _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 2us; 65535us; 0us; 1us; 6us; 8us; 1us; 65535us; 4us; 5us; 3us; 65535us; 10us; 11us; 13us; 14us; 15us; 16us; 3us; 65535us; 10us; 15us; 13us; 15us; 15us; 15us; 3us; 65535us; 4us; 10us; 12us; 13us; 43us; 44us; 3us; 65535us; 4us; 43us; 12us; 43us; 43us; 43us; 2us; 65535us; 46us; 47us; 49us; 50us; 12us; 65535us; 25us; 26us; 27us; 28us; 33us; 34us; 35us; 36us; 41us; 42us; 46us; 48us; 49us; 48us; 59us; 54us; 60us; 55us; 61us; 56us; 62us; 57us; 64us; 58us; 10us; 65535us; 4us; 45us; 12us; 45us; 23us; 24us; 31us; 32us; 39us; 40us; 43us; 45us; 81us; 77us; 82us; 78us; 83us; 79us; 84us; 80us; 2us; 65535us; 4us; 12us; 86us; 87us; 2us; 65535us; 4us; 86us; 86us; 86us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 4us; 6us; 10us; 14us; 18us; 22us; 25us; 38us; 49us; 52us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 2us; 1us; 2us; 2us; 1us; 2us; 2us; 1us; 2us; 2us; 1us; 2us; 2us; 1us; 2us; 1us; 1us; 1us; 2us; 1us; 2us; 2us; 3us; 4us; 1us; 4us; 2us; 5us; 6us; 2us; 5us; 6us; 1us; 6us; 2us; 7us; 8us; 1us; 8us; 1us; 9us; 1us; 10us; 1us; 10us; 1us; 10us; 1us; 10us; 1us; 11us; 1us; 11us; 3us; 11us; 31us; 32us; 1us; 11us; 5us; 11us; 21us; 22us; 23us; 25us; 1us; 11us; 5us; 11us; 21us; 22us; 23us; 25us; 1us; 11us; 1us; 12us; 1us; 12us; 3us; 12us; 31us; 32us; 1us; 12us; 5us; 12us; 21us; 22us; 23us; 25us; 1us; 12us; 5us; 12us; 21us; 22us; 23us; 25us; 1us; 12us; 1us; 13us; 1us; 13us; 3us; 13us; 31us; 32us; 1us; 13us; 5us; 13us; 21us; 22us; 23us; 25us; 2us; 14us; 15us; 1us; 15us; 3us; 16us; 31us; 32us; 1us; 16us; 1us; 16us; 6us; 17us; 18us; 21us; 22us; 23us; 25us; 1us; 18us; 1us; 18us; 1us; 19us; 1us; 20us; 1us; 20us; 5us; 21us; 21us; 22us; 23us; 25us; 5us; 21us; 22us; 22us; 23us; 25us; 5us; 21us; 22us; 23us; 23us; 25us; 5us; 21us; 22us; 23us; 24us; 25us; 5us; 21us; 22us; 23us; 25us; 26us; 1us; 21us; 1us; 22us; 1us; 23us; 1us; 24us; 1us; 25us; 1us; 26us; 1us; 26us; 2us; 27us; 28us; 2us; 27us; 28us; 2us; 27us; 28us; 2us; 27us; 28us; 2us; 27us; 28us; 2us; 27us; 28us; 2us; 27us; 28us; 1us; 28us; 1us; 28us; 1us; 29us; 1us; 30us; 3us; 31us; 31us; 32us; 3us; 31us; 32us; 32us; 3us; 31us; 32us; 33us; 3us; 31us; 32us; 34us; 1us; 31us; 1us; 32us; 1us; 33us; 1us; 34us; 1us; 34us; 2us; 35us; 36us; 1us; 36us; 1us; 37us; 1us; 37us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 7us; 10us; 13us; 16us; 19us; 21us; 23us; 25us; 28us; 30us; 33us; 36us; 38us; 41us; 43us; 45us; 47us; 49us; 51us; 53us; 55us; 57us; 61us; 63us; 69us; 71us; 77us; 79us; 81us; 83us; 87us; 89us; 95us; 97us; 103us; 105us; 107us; 109us; 113us; 115us; 121us; 124us; 126us; 130us; 132us; 134us; 141us; 143us; 145us; 147us; 149us; 151us; 157us; 163us; 169us; 175us; 181us; 183us; 185us; 187us; 189us; 191us; 193us; 195us; 198us; 201us; 204us; 207us; 210us; 213us; 216us; 218us; 220us; 222us; 224us; 228us; 232us; 236us; 240us; 242us; 244us; 246us; 248us; 250us; 253us; 255us; 257us; |]
let _fsyacc_action_rows = 90
let _fsyacc_actionTableElements = [|1us; 32768us; 1us; 2us; 0us; 49152us; 1us; 32768us; 36us; 3us; 1us; 32768us; 25us; 4us; 6us; 32768us; 0us; 88us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 1us; 32768us; 26us; 6us; 2us; 32768us; 1us; 2us; 34us; 7us; 0us; 16385us; 1us; 32768us; 34us; 9us; 0us; 16386us; 5us; 16387us; 2us; 38us; 6us; 22us; 7us; 30us; 8us; 18us; 9us; 17us; 0us; 16388us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 5us; 16389us; 2us; 38us; 6us; 22us; 7us; 30us; 8us; 18us; 9us; 17us; 0us; 16390us; 5us; 16391us; 2us; 38us; 6us; 22us; 7us; 30us; 8us; 18us; 9us; 17us; 0us; 16392us; 0us; 16393us; 1us; 32768us; 21us; 19us; 1us; 32768us; 35us; 20us; 1us; 32768us; 22us; 21us; 0us; 16394us; 1us; 32768us; 21us; 23us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 3us; 32768us; 16us; 82us; 17us; 81us; 27us; 25us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 5us; 32768us; 11us; 60us; 12us; 61us; 14us; 63us; 30us; 59us; 31us; 27us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 5us; 32768us; 11us; 60us; 12us; 61us; 14us; 63us; 22us; 29us; 30us; 59us; 0us; 16395us; 1us; 32768us; 21us; 31us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 3us; 32768us; 16us; 82us; 17us; 81us; 27us; 33us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 5us; 32768us; 11us; 60us; 12us; 61us; 14us; 63us; 30us; 59us; 31us; 35us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 5us; 32768us; 11us; 60us; 12us; 61us; 14us; 63us; 22us; 37us; 30us; 59us; 0us; 16396us; 1us; 32768us; 21us; 39us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 3us; 32768us; 16us; 82us; 17us; 81us; 27us; 41us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 4us; 16397us; 11us; 60us; 12us; 61us; 14us; 63us; 30us; 59us; 5us; 16398us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 0us; 16399us; 3us; 32768us; 16us; 82us; 17us; 81us; 32us; 46us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 0us; 16400us; 5us; 16401us; 11us; 60us; 12us; 61us; 14us; 63us; 15us; 49us; 30us; 59us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 0us; 16402us; 0us; 16403us; 1us; 32768us; 24us; 53us; 0us; 16404us; 1us; 16405us; 14us; 63us; 2us; 16406us; 14us; 63us; 30us; 59us; 2us; 16407us; 14us; 63us; 30us; 59us; 1us; 16408us; 14us; 63us; 5us; 32768us; 11us; 60us; 12us; 61us; 14us; 63us; 22us; 65us; 30us; 59us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 0us; 16409us; 4us; 32768us; 13us; 62us; 21us; 64us; 23us; 52us; 36us; 51us; 0us; 16410us; 1us; 32768us; 33us; 67us; 1us; 32768us; 35us; 68us; 1us; 32768us; 33us; 69us; 1us; 32768us; 35us; 70us; 1us; 32768us; 33us; 71us; 1us; 32768us; 35us; 72us; 1us; 16411us; 28us; 73us; 1us; 32768us; 35us; 74us; 0us; 16412us; 0us; 16413us; 0us; 16414us; 0us; 16415us; 0us; 16416us; 0us; 16417us; 3us; 32768us; 16us; 82us; 17us; 81us; 22us; 85us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 5us; 32768us; 13us; 83us; 18us; 75us; 19us; 76us; 21us; 84us; 35us; 66us; 0us; 16418us; 1us; 16419us; 0us; 88us; 0us; 16420us; 1us; 32768us; 36us; 89us; 0us; 16421us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 2us; 3us; 5us; 7us; 14us; 16us; 19us; 20us; 22us; 23us; 29us; 30us; 36us; 42us; 43us; 49us; 50us; 51us; 53us; 55us; 57us; 58us; 60us; 66us; 70us; 75us; 81us; 86us; 92us; 93us; 95us; 101us; 105us; 110us; 116us; 121us; 127us; 128us; 130us; 136us; 140us; 145us; 150us; 156us; 157us; 161us; 166us; 167us; 173us; 178us; 179us; 180us; 182us; 183us; 185us; 188us; 191us; 193us; 199us; 204us; 209us; 214us; 219us; 220us; 225us; 226us; 228us; 230us; 232us; 234us; 236us; 238us; 240us; 242us; 243us; 244us; 245us; 246us; 247us; 248us; 252us; 258us; 264us; 270us; 276us; 277us; 279us; 280us; 282us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 6us; 7us; 1us; 2us; 2us; 3us; 1us; 2us; 1us; 4us; 8us; 8us; 5us; 1us; 2us; 3us; 1us; 3us; 1us; 2us; 3us; 3us; 3us; 2us; 2us; 3us; 7us; 9us; 1us; 1us; 3us; 3us; 2us; 3us; 1us; 2us; 2us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 1us; 2us; 2us; 2us; 2us; 3us; 3us; 4us; 4us; 4us; 4us; 4us; 5us; 5us; 6us; 7us; 7us; 8us; 8us; 8us; 8us; 8us; 8us; 8us; 8us; 9us; 9us; 9us; 9us; 9us; 9us; 9us; 9us; 10us; 10us; 11us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 65535us; 65535us; 65535us; 65535us; 65535us; 16385us; 65535us; 16386us; 65535us; 16388us; 65535us; 65535us; 16390us; 65535us; 16392us; 16393us; 65535us; 65535us; 65535us; 16394us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16395us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16396us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16399us; 65535us; 65535us; 16400us; 65535us; 65535us; 16402us; 16403us; 65535us; 16404us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16409us; 65535us; 16410us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16412us; 16413us; 16414us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16418us; 65535us; 16420us; 65535us; 16421us; |]
let _fsyacc_reductions ()  =    [| 
# 346 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Ast.T)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (Microsoft.FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startstart));
# 355 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'scope)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 44 "Parser.fsy"
                                                            [addScope _2 _4] 
                   )
# 44 "Parser.fsy"
                 : Ast.T));
# 367 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'scope)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : Ast.T)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 45 "Parser.fsy"
                                                                (addScope _2 _4) :: _6 
                   )
# 45 "Parser.fsy"
                 : Ast.T));
# 380 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 48 "Parser.fsy"
                                          ([], _1, []) 
                   )
# 48 "Parser.fsy"
                 : 'scope));
# 391 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 49 "Parser.fsy"
                                                 ([], _1, _2) 
                   )
# 49 "Parser.fsy"
                 : 'scope));
# 403 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definitions)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 50 "Parser.fsy"
                                                   (_1, _2, []) 
                   )
# 50 "Parser.fsy"
                 : 'scope));
# 415 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definitions)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 51 "Parser.fsy"
                                                          (_1, _2, _3) 
                   )
# 51 "Parser.fsy"
                 : 'scope));
# 428 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 54 "Parser.fsy"
                                         [_1] 
                   )
# 54 "Parser.fsy"
                 : 'cconstrs));
# 439 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 55 "Parser.fsy"
                                                _1 :: _2 
                   )
# 55 "Parser.fsy"
                 : 'cconstrs));
# 451 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 58 "Parser.fsy"
                                           Ast.Multipath 
                   )
# 58 "Parser.fsy"
                 : 'cconstr));
# 461 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 59 "Parser.fsy"
                                                        Ast.MaxRoutes _3 
                   )
# 59 "Parser.fsy"
                 : 'cconstr));
# 472 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 60 "Parser.fsy"
                                                                                     Ast.RouteAggregate(_3, _5, _7) 
                   )
# 60 "Parser.fsy"
                 : 'cconstr));
# 485 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 61 "Parser.fsy"
                                                                                     Ast.CommunityTag(_3, _5, _7) 
                   )
# 61 "Parser.fsy"
                 : 'cconstr));
# 498 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 62 "Parser.fsy"
                                                             Ast.Ownership(_3, _5) 
                   )
# 62 "Parser.fsy"
                 : 'cconstr));
# 510 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 65 "Parser.fsy"
                                         [_1] 
                   )
# 65 "Parser.fsy"
                 : 'pconstrs));
# 521 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 66 "Parser.fsy"
                                                _1 :: _2 
                   )
# 66 "Parser.fsy"
                 : 'pconstrs));
# 533 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regexs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 69 "Parser.fsy"
                                                     Ast.Path(_1, _3) 
                   )
# 69 "Parser.fsy"
                 : 'pconstr));
# 545 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 72 "Parser.fsy"
                                        [_1] 
                   )
# 72 "Parser.fsy"
                 : 'regexs));
# 556 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regexs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 73 "Parser.fsy"
                                                _1 :: _3 
                   )
# 73 "Parser.fsy"
                 : 'regexs));
# 568 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 76 "Parser.fsy"
                                     loc _1 
                   )
# 76 "Parser.fsy"
                 : 'regex));
# 579 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 77 "Parser.fsy"
                                                 Ast.Empty 
                   )
# 77 "Parser.fsy"
                 : 'regex));
# 589 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 78 "Parser.fsy"
                                                    Ast.Concat (_1, _3) 
                   )
# 78 "Parser.fsy"
                 : 'regex));
# 601 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 79 "Parser.fsy"
                                                    Ast.Inter (_1, _3) 
                   )
# 79 "Parser.fsy"
                 : 'regex));
# 613 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 80 "Parser.fsy"
                                                 Ast.Union (_1, _3) 
                   )
# 80 "Parser.fsy"
                 : 'regex));
# 625 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 81 "Parser.fsy"
                                           Ast.Negate _2 
                   )
# 81 "Parser.fsy"
                 : 'regex));
# 636 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 82 "Parser.fsy"
                                           Ast.Star _1 
                   )
# 82 "Parser.fsy"
                 : 'regex));
# 647 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 83 "Parser.fsy"
                                                  _2 
                   )
# 83 "Parser.fsy"
                 : 'regex));
# 658 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 86 "Parser.fsy"
                                                        Prefix.Prefix(_1, _3, _5, _7, None) 
                   )
# 86 "Parser.fsy"
                 : 'predicate));
# 672 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _9 = (let data = parseState.GetInput(9) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 87 "Parser.fsy"
                                                                Prefix.Prefix(_1, _3, _5, _7, Some _9) 
                   )
# 87 "Parser.fsy"
                 : 'predicate));
# 687 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 88 "Parser.fsy"
                                       Prefix.True 
                   )
# 88 "Parser.fsy"
                 : 'predicate));
# 697 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 89 "Parser.fsy"
                                        Prefix.False 
                   )
# 89 "Parser.fsy"
                 : 'predicate));
# 707 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 90 "Parser.fsy"
                                                    Prefix.Or(_1, _3) 
                   )
# 90 "Parser.fsy"
                 : 'predicate));
# 719 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 91 "Parser.fsy"
                                                     Prefix.And(_1, _3) 
                   )
# 91 "Parser.fsy"
                 : 'predicate));
# 731 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 92 "Parser.fsy"
                                              Prefix.Not _2 
                   )
# 92 "Parser.fsy"
                 : 'predicate));
# 742 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 93 "Parser.fsy"
                                                     _2 
                   )
# 93 "Parser.fsy"
                 : 'predicate));
# 753 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definition)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 97 "Parser.fsy"
                                           [_1] 
                   )
# 97 "Parser.fsy"
                 : 'definitions));
# 764 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definition)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'definitions)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 98 "Parser.fsy"
                                                    _1 :: _2 
                   )
# 98 "Parser.fsy"
                 : 'definitions));
# 776 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 101 "Parser.fsy"
                                           _2 
                   )
# 101 "Parser.fsy"
                 : 'definition));
|]
# 788 "Parser.fs"
let tables () : Microsoft.FSharp.Text.Parsing.Tables<_> = 
  { reductions= _fsyacc_reductions ();
    endOfInputTag = _fsyacc_endOfInputTag;
    tagOfToken = tagOfToken;
    dataOfToken = _fsyacc_dataOfToken; 
    actionTableElements = _fsyacc_actionTableElements;
    actionTableRowOffsets = _fsyacc_actionTableRowOffsets;
    stateToProdIdxsTableElements = _fsyacc_stateToProdIdxsTableElements;
    stateToProdIdxsTableRowOffsets = _fsyacc_stateToProdIdxsTableRowOffsets;
    reductionSymbolCounts = _fsyacc_reductionSymbolCounts;
    immediateActions = _fsyacc_immediateActions;
    gotos = _fsyacc_gotos;
    sparseGotoTableRowOffsets = _fsyacc_sparseGotoTableRowOffsets;
    tagOfErrorTerminal = _fsyacc_tagOfErrorTerminal;
    parseError = (fun (ctxt:Microsoft.FSharp.Text.Parsing.ParseErrorContext<_>) -> 
                              match parse_error_rich with 
                              | Some f -> f ctxt
                              | None -> parse_error ctxt.Message);
    numTerminals = 40;
    productionToNonTerminalTable = _fsyacc_productionToNonTerminalTable  }
let engine lexer lexbuf startState = (tables ()).Interpret(lexer, lexbuf, startState)
let start lexer lexbuf : Ast.T =
    Microsoft.FSharp.Core.Operators.unbox ((tables ()).Interpret(lexer, lexbuf, 0))
