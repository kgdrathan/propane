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
  | INCLUDE
  | MULTIPATH
  | MAXROUTES
  | COMMUNITY
  | AGGREGATE
  | UNORDERED
  | ORDERED
  | PATHS
  | OWNS
  | SCOPE
  | DEFINE
  | FALSE
  | TRUE
  | OR
  | AND
  | SHR
  | STAR
  | NOT
  | UNION
  | INTERSECT
  | EOF
  | DOT
  | ROCKET
  | RARROW
  | SEMICOLON
  | COLON
  | SLASH
  | COMMA
  | RBRACE
  | LBRACE
  | RBRACKET
  | LBRACKET
  | RPAREN
  | LPAREN
  | EQUAL
  | INT of (System.Int32)
  | ID of (string)
// This type is used to give symbolic names to token indexes, useful for error messages
type tokenId = 
    | TOKEN_INCLUDE
    | TOKEN_MULTIPATH
    | TOKEN_MAXROUTES
    | TOKEN_COMMUNITY
    | TOKEN_AGGREGATE
    | TOKEN_UNORDERED
    | TOKEN_ORDERED
    | TOKEN_PATHS
    | TOKEN_OWNS
    | TOKEN_SCOPE
    | TOKEN_DEFINE
    | TOKEN_FALSE
    | TOKEN_TRUE
    | TOKEN_OR
    | TOKEN_AND
    | TOKEN_SHR
    | TOKEN_STAR
    | TOKEN_NOT
    | TOKEN_UNION
    | TOKEN_INTERSECT
    | TOKEN_EOF
    | TOKEN_DOT
    | TOKEN_ROCKET
    | TOKEN_RARROW
    | TOKEN_SEMICOLON
    | TOKEN_COLON
    | TOKEN_SLASH
    | TOKEN_COMMA
    | TOKEN_RBRACE
    | TOKEN_LBRACE
    | TOKEN_RBRACKET
    | TOKEN_LBRACKET
    | TOKEN_RPAREN
    | TOKEN_LPAREN
    | TOKEN_EQUAL
    | TOKEN_INT
    | TOKEN_ID
    | TOKEN_end_of_input
    | TOKEN_error
// This type is used to give symbolic names to token indexes, useful for error messages
type nonTerminalId = 
    | NONTERM__startstart
    | NONTERM_start
    | NONTERM_scopes
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
  | INCLUDE  -> 0 
  | MULTIPATH  -> 1 
  | MAXROUTES  -> 2 
  | COMMUNITY  -> 3 
  | AGGREGATE  -> 4 
  | UNORDERED  -> 5 
  | ORDERED  -> 6 
  | PATHS  -> 7 
  | OWNS  -> 8 
  | SCOPE  -> 9 
  | DEFINE  -> 10 
  | FALSE  -> 11 
  | TRUE  -> 12 
  | OR  -> 13 
  | AND  -> 14 
  | SHR  -> 15 
  | STAR  -> 16 
  | NOT  -> 17 
  | UNION  -> 18 
  | INTERSECT  -> 19 
  | EOF  -> 20 
  | DOT  -> 21 
  | ROCKET  -> 22 
  | RARROW  -> 23 
  | SEMICOLON  -> 24 
  | COLON  -> 25 
  | SLASH  -> 26 
  | COMMA  -> 27 
  | RBRACE  -> 28 
  | LBRACE  -> 29 
  | RBRACKET  -> 30 
  | LBRACKET  -> 31 
  | RPAREN  -> 32 
  | LPAREN  -> 33 
  | EQUAL  -> 34 
  | INT _ -> 35 
  | ID _ -> 36 

// This function maps integer indexes to symbolic token ids
let tokenTagToTokenId (tokenIdx:int) = 
  match tokenIdx with
  | 0 -> TOKEN_INCLUDE 
  | 1 -> TOKEN_MULTIPATH 
  | 2 -> TOKEN_MAXROUTES 
  | 3 -> TOKEN_COMMUNITY 
  | 4 -> TOKEN_AGGREGATE 
  | 5 -> TOKEN_UNORDERED 
  | 6 -> TOKEN_ORDERED 
  | 7 -> TOKEN_PATHS 
  | 8 -> TOKEN_OWNS 
  | 9 -> TOKEN_SCOPE 
  | 10 -> TOKEN_DEFINE 
  | 11 -> TOKEN_FALSE 
  | 12 -> TOKEN_TRUE 
  | 13 -> TOKEN_OR 
  | 14 -> TOKEN_AND 
  | 15 -> TOKEN_SHR 
  | 16 -> TOKEN_STAR 
  | 17 -> TOKEN_NOT 
  | 18 -> TOKEN_UNION 
  | 19 -> TOKEN_INTERSECT 
  | 20 -> TOKEN_EOF 
  | 21 -> TOKEN_DOT 
  | 22 -> TOKEN_ROCKET 
  | 23 -> TOKEN_RARROW 
  | 24 -> TOKEN_SEMICOLON 
  | 25 -> TOKEN_COLON 
  | 26 -> TOKEN_SLASH 
  | 27 -> TOKEN_COMMA 
  | 28 -> TOKEN_RBRACE 
  | 29 -> TOKEN_LBRACE 
  | 30 -> TOKEN_RBRACKET 
  | 31 -> TOKEN_LBRACKET 
  | 32 -> TOKEN_RPAREN 
  | 33 -> TOKEN_LPAREN 
  | 34 -> TOKEN_EQUAL 
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
    | 2 -> NONTERM_scopes 
    | 3 -> NONTERM_scopes 
    | 4 -> NONTERM_scope 
    | 5 -> NONTERM_scope 
    | 6 -> NONTERM_scope 
    | 7 -> NONTERM_scope 
    | 8 -> NONTERM_cconstrs 
    | 9 -> NONTERM_cconstrs 
    | 10 -> NONTERM_cconstr 
    | 11 -> NONTERM_cconstr 
    | 12 -> NONTERM_cconstr 
    | 13 -> NONTERM_cconstr 
    | 14 -> NONTERM_cconstr 
    | 15 -> NONTERM_pconstrs 
    | 16 -> NONTERM_pconstrs 
    | 17 -> NONTERM_pconstr 
    | 18 -> NONTERM_regexs 
    | 19 -> NONTERM_regexs 
    | 20 -> NONTERM_regex 
    | 21 -> NONTERM_regex 
    | 22 -> NONTERM_regex 
    | 23 -> NONTERM_regex 
    | 24 -> NONTERM_regex 
    | 25 -> NONTERM_regex 
    | 26 -> NONTERM_regex 
    | 27 -> NONTERM_regex 
    | 28 -> NONTERM_predicate 
    | 29 -> NONTERM_predicate 
    | 30 -> NONTERM_predicate 
    | 31 -> NONTERM_predicate 
    | 32 -> NONTERM_predicate 
    | 33 -> NONTERM_predicate 
    | 34 -> NONTERM_predicate 
    | 35 -> NONTERM_predicate 
    | 36 -> NONTERM_definitions 
    | 37 -> NONTERM_definitions 
    | 38 -> NONTERM_definition 
    | _ -> failwith "prodIdxToNonTerminal: bad production index"

let _fsyacc_endOfInputTag = 39 
let _fsyacc_tagOfErrorTerminal = 37

// This function gets the name of a token as a string
let token_to_string (t:token) = 
  match t with 
  | INCLUDE  -> "INCLUDE" 
  | MULTIPATH  -> "MULTIPATH" 
  | MAXROUTES  -> "MAXROUTES" 
  | COMMUNITY  -> "COMMUNITY" 
  | AGGREGATE  -> "AGGREGATE" 
  | UNORDERED  -> "UNORDERED" 
  | ORDERED  -> "ORDERED" 
  | PATHS  -> "PATHS" 
  | OWNS  -> "OWNS" 
  | SCOPE  -> "SCOPE" 
  | DEFINE  -> "DEFINE" 
  | FALSE  -> "FALSE" 
  | TRUE  -> "TRUE" 
  | OR  -> "OR" 
  | AND  -> "AND" 
  | SHR  -> "SHR" 
  | STAR  -> "STAR" 
  | NOT  -> "NOT" 
  | UNION  -> "UNION" 
  | INTERSECT  -> "INTERSECT" 
  | EOF  -> "EOF" 
  | DOT  -> "DOT" 
  | ROCKET  -> "ROCKET" 
  | RARROW  -> "RARROW" 
  | SEMICOLON  -> "SEMICOLON" 
  | COLON  -> "COLON" 
  | SLASH  -> "SLASH" 
  | COMMA  -> "COMMA" 
  | RBRACE  -> "RBRACE" 
  | LBRACE  -> "LBRACE" 
  | RBRACKET  -> "RBRACKET" 
  | LBRACKET  -> "LBRACKET" 
  | RPAREN  -> "RPAREN" 
  | LPAREN  -> "LPAREN" 
  | EQUAL  -> "EQUAL" 
  | INT _ -> "INT" 
  | ID _ -> "ID" 

// This function gets the data carried by a token as an object
let _fsyacc_dataOfToken (t:token) = 
  match t with 
  | INCLUDE  -> (null : System.Object) 
  | MULTIPATH  -> (null : System.Object) 
  | MAXROUTES  -> (null : System.Object) 
  | COMMUNITY  -> (null : System.Object) 
  | AGGREGATE  -> (null : System.Object) 
  | UNORDERED  -> (null : System.Object) 
  | ORDERED  -> (null : System.Object) 
  | PATHS  -> (null : System.Object) 
  | OWNS  -> (null : System.Object) 
  | SCOPE  -> (null : System.Object) 
  | DEFINE  -> (null : System.Object) 
  | FALSE  -> (null : System.Object) 
  | TRUE  -> (null : System.Object) 
  | OR  -> (null : System.Object) 
  | AND  -> (null : System.Object) 
  | SHR  -> (null : System.Object) 
  | STAR  -> (null : System.Object) 
  | NOT  -> (null : System.Object) 
  | UNION  -> (null : System.Object) 
  | INTERSECT  -> (null : System.Object) 
  | EOF  -> (null : System.Object) 
  | DOT  -> (null : System.Object) 
  | ROCKET  -> (null : System.Object) 
  | RARROW  -> (null : System.Object) 
  | SEMICOLON  -> (null : System.Object) 
  | COLON  -> (null : System.Object) 
  | SLASH  -> (null : System.Object) 
  | COMMA  -> (null : System.Object) 
  | RBRACE  -> (null : System.Object) 
  | LBRACE  -> (null : System.Object) 
  | RBRACKET  -> (null : System.Object) 
  | LBRACKET  -> (null : System.Object) 
  | RPAREN  -> (null : System.Object) 
  | LPAREN  -> (null : System.Object) 
  | EQUAL  -> (null : System.Object) 
  | INT _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
  | ID _fsyacc_x -> Microsoft.FSharp.Core.Operators.box _fsyacc_x 
let _fsyacc_gotos = [| 0us; 65535us; 1us; 65535us; 0us; 1us; 2us; 65535us; 0us; 2us; 8us; 9us; 1us; 65535us; 6us; 7us; 3us; 65535us; 10us; 11us; 13us; 14us; 15us; 16us; 3us; 65535us; 10us; 15us; 13us; 15us; 15us; 15us; 3us; 65535us; 6us; 10us; 12us; 13us; 44us; 45us; 3us; 65535us; 6us; 44us; 12us; 44us; 44us; 44us; 2us; 65535us; 47us; 48us; 50us; 51us; 12us; 65535us; 25us; 26us; 27us; 28us; 33us; 34us; 35us; 36us; 41us; 42us; 47us; 49us; 50us; 49us; 60us; 55us; 61us; 56us; 62us; 57us; 63us; 58us; 65us; 59us; 10us; 65535us; 6us; 46us; 12us; 46us; 23us; 24us; 31us; 32us; 39us; 40us; 44us; 46us; 82us; 78us; 83us; 79us; 84us; 80us; 85us; 81us; 2us; 65535us; 6us; 12us; 87us; 88us; 2us; 65535us; 6us; 87us; 87us; 87us; |]
let _fsyacc_sparseGotoTableRowOffsets = [|0us; 1us; 3us; 6us; 8us; 12us; 16us; 20us; 24us; 27us; 40us; 51us; 54us; |]
let _fsyacc_stateToProdIdxsTableElements = [| 1us; 0us; 1us; 0us; 1us; 1us; 1us; 1us; 2us; 2us; 3us; 2us; 2us; 3us; 2us; 2us; 3us; 2us; 2us; 3us; 2us; 2us; 3us; 1us; 3us; 2us; 4us; 5us; 1us; 5us; 2us; 6us; 7us; 2us; 6us; 7us; 1us; 7us; 2us; 8us; 9us; 1us; 9us; 1us; 10us; 1us; 11us; 1us; 11us; 1us; 11us; 1us; 11us; 1us; 12us; 1us; 12us; 3us; 12us; 32us; 33us; 1us; 12us; 5us; 12us; 22us; 23us; 24us; 26us; 1us; 12us; 5us; 12us; 22us; 23us; 24us; 26us; 1us; 12us; 1us; 13us; 1us; 13us; 3us; 13us; 32us; 33us; 1us; 13us; 5us; 13us; 22us; 23us; 24us; 26us; 1us; 13us; 5us; 13us; 22us; 23us; 24us; 26us; 1us; 13us; 1us; 14us; 1us; 14us; 3us; 14us; 32us; 33us; 1us; 14us; 5us; 14us; 22us; 23us; 24us; 26us; 1us; 14us; 2us; 15us; 16us; 1us; 16us; 3us; 17us; 32us; 33us; 1us; 17us; 1us; 17us; 6us; 18us; 19us; 22us; 23us; 24us; 26us; 1us; 19us; 1us; 19us; 1us; 20us; 1us; 21us; 1us; 21us; 5us; 22us; 22us; 23us; 24us; 26us; 5us; 22us; 23us; 23us; 24us; 26us; 5us; 22us; 23us; 24us; 24us; 26us; 5us; 22us; 23us; 24us; 25us; 26us; 5us; 22us; 23us; 24us; 26us; 27us; 1us; 22us; 1us; 23us; 1us; 24us; 1us; 25us; 1us; 26us; 1us; 27us; 1us; 27us; 2us; 28us; 29us; 2us; 28us; 29us; 2us; 28us; 29us; 2us; 28us; 29us; 2us; 28us; 29us; 2us; 28us; 29us; 2us; 28us; 29us; 1us; 29us; 1us; 29us; 1us; 30us; 1us; 31us; 3us; 32us; 32us; 33us; 3us; 32us; 33us; 33us; 3us; 32us; 33us; 34us; 3us; 32us; 33us; 35us; 1us; 32us; 1us; 33us; 1us; 34us; 1us; 35us; 1us; 35us; 2us; 36us; 37us; 1us; 37us; 1us; 38us; 1us; 38us; |]
let _fsyacc_stateToProdIdxsTableRowOffsets = [|0us; 2us; 4us; 6us; 8us; 11us; 14us; 17us; 20us; 23us; 25us; 28us; 30us; 33us; 36us; 38us; 41us; 43us; 45us; 47us; 49us; 51us; 53us; 55us; 57us; 61us; 63us; 69us; 71us; 77us; 79us; 81us; 83us; 87us; 89us; 95us; 97us; 103us; 105us; 107us; 109us; 113us; 115us; 121us; 123us; 126us; 128us; 132us; 134us; 136us; 143us; 145us; 147us; 149us; 151us; 153us; 159us; 165us; 171us; 177us; 183us; 185us; 187us; 189us; 191us; 193us; 195us; 197us; 200us; 203us; 206us; 209us; 212us; 215us; 218us; 220us; 222us; 224us; 226us; 230us; 234us; 238us; 242us; 244us; 246us; 248us; 250us; 252us; 255us; 257us; 259us; |]
let _fsyacc_action_rows = 91
let _fsyacc_actionTableElements = [|1us; 32768us; 9us; 4us; 0us; 49152us; 1us; 32768us; 20us; 3us; 0us; 16385us; 1us; 32768us; 36us; 5us; 1us; 32768us; 29us; 6us; 6us; 32768us; 10us; 89us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 1us; 32768us; 28us; 8us; 1us; 16386us; 9us; 4us; 0us; 16387us; 5us; 16388us; 1us; 17us; 2us; 18us; 3us; 30us; 4us; 22us; 8us; 38us; 0us; 16389us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 5us; 16390us; 1us; 17us; 2us; 18us; 3us; 30us; 4us; 22us; 8us; 38us; 0us; 16391us; 5us; 16392us; 1us; 17us; 2us; 18us; 3us; 30us; 4us; 22us; 8us; 38us; 0us; 16393us; 0us; 16394us; 1us; 32768us; 33us; 19us; 1us; 32768us; 35us; 20us; 1us; 32768us; 32us; 21us; 0us; 16395us; 1us; 32768us; 33us; 23us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 3us; 32768us; 13us; 82us; 14us; 83us; 27us; 25us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 5us; 32768us; 16us; 64us; 18us; 62us; 19us; 61us; 23us; 27us; 24us; 60us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 5us; 32768us; 16us; 64us; 18us; 62us; 19us; 61us; 24us; 60us; 32us; 29us; 0us; 16396us; 1us; 32768us; 33us; 31us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 3us; 32768us; 13us; 82us; 14us; 83us; 27us; 33us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 5us; 32768us; 16us; 64us; 18us; 62us; 19us; 61us; 23us; 35us; 24us; 60us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 5us; 32768us; 16us; 64us; 18us; 62us; 19us; 61us; 24us; 60us; 32us; 37us; 0us; 16397us; 1us; 32768us; 33us; 39us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 3us; 32768us; 13us; 82us; 14us; 83us; 27us; 41us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 5us; 32768us; 16us; 64us; 18us; 62us; 19us; 61us; 24us; 60us; 32us; 43us; 0us; 16398us; 5us; 16399us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 0us; 16400us; 3us; 32768us; 13us; 82us; 14us; 83us; 22us; 47us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 0us; 16401us; 5us; 16402us; 15us; 50us; 16us; 64us; 18us; 62us; 19us; 61us; 24us; 60us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 0us; 16403us; 0us; 16404us; 1us; 32768us; 30us; 54us; 0us; 16405us; 1us; 16406us; 16us; 64us; 2us; 16407us; 16us; 64us; 24us; 60us; 2us; 16408us; 16us; 64us; 24us; 60us; 1us; 16409us; 16us; 64us; 5us; 32768us; 16us; 64us; 18us; 62us; 19us; 61us; 24us; 60us; 32us; 66us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 0us; 16410us; 4us; 32768us; 17us; 63us; 31us; 53us; 33us; 65us; 36us; 52us; 0us; 16411us; 1us; 32768us; 21us; 68us; 1us; 32768us; 35us; 69us; 1us; 32768us; 21us; 70us; 1us; 32768us; 35us; 71us; 1us; 32768us; 21us; 72us; 1us; 32768us; 35us; 73us; 1us; 16412us; 26us; 74us; 1us; 32768us; 35us; 75us; 0us; 16413us; 0us; 16414us; 0us; 16415us; 0us; 16416us; 0us; 16417us; 0us; 16418us; 3us; 32768us; 13us; 82us; 14us; 83us; 32us; 86us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 5us; 32768us; 11us; 77us; 12us; 76us; 17us; 84us; 33us; 85us; 35us; 67us; 0us; 16419us; 1us; 16420us; 10us; 89us; 0us; 16421us; 1us; 32768us; 36us; 90us; 0us; 16422us; |]
let _fsyacc_actionTableRowOffsets = [|0us; 2us; 3us; 5us; 6us; 8us; 10us; 17us; 19us; 21us; 22us; 28us; 29us; 35us; 41us; 42us; 48us; 49us; 50us; 52us; 54us; 56us; 57us; 59us; 65us; 69us; 74us; 80us; 85us; 91us; 92us; 94us; 100us; 104us; 109us; 115us; 120us; 126us; 127us; 129us; 135us; 139us; 144us; 150us; 151us; 157us; 158us; 162us; 167us; 168us; 174us; 179us; 180us; 181us; 183us; 184us; 186us; 189us; 192us; 194us; 200us; 205us; 210us; 215us; 220us; 221us; 226us; 227us; 229us; 231us; 233us; 235us; 237us; 239us; 241us; 243us; 244us; 245us; 246us; 247us; 248us; 249us; 253us; 259us; 265us; 271us; 277us; 278us; 280us; 281us; 283us; |]
let _fsyacc_reductionSymbolCounts = [|1us; 2us; 5us; 6us; 1us; 2us; 2us; 3us; 1us; 2us; 1us; 4us; 8us; 8us; 6us; 1us; 2us; 3us; 1us; 3us; 1us; 2us; 3us; 3us; 3us; 2us; 2us; 3us; 7us; 9us; 1us; 1us; 3us; 3us; 2us; 3us; 1us; 2us; 2us; |]
let _fsyacc_productionToNonTerminalTable = [|0us; 1us; 2us; 2us; 3us; 3us; 3us; 3us; 4us; 4us; 5us; 5us; 5us; 5us; 5us; 6us; 6us; 7us; 8us; 8us; 9us; 9us; 9us; 9us; 9us; 9us; 9us; 9us; 10us; 10us; 10us; 10us; 10us; 10us; 10us; 10us; 11us; 11us; 12us; |]
let _fsyacc_immediateActions = [|65535us; 49152us; 65535us; 16385us; 65535us; 65535us; 65535us; 65535us; 65535us; 16387us; 65535us; 16389us; 65535us; 65535us; 16391us; 65535us; 16393us; 16394us; 65535us; 65535us; 65535us; 16395us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16396us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16397us; 65535us; 65535us; 65535us; 65535us; 65535us; 16398us; 65535us; 16400us; 65535us; 65535us; 16401us; 65535us; 65535us; 16403us; 16404us; 65535us; 16405us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16410us; 65535us; 16411us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16413us; 16414us; 16415us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 65535us; 16419us; 65535us; 16421us; 65535us; 16422us; |]
let _fsyacc_reductions ()  =    [| 
# 348 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : Ast.T)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
                      raise (Microsoft.FSharp.Text.Parsing.Accept(Microsoft.FSharp.Core.Operators.box _1))
                   )
                 : '_startstart));
# 357 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'scopes)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 78 "Parser.fsy"
                                           _1 
                   )
# 78 "Parser.fsy"
                 : Ast.T));
# 368 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'scope)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 81 "Parser.fsy"
                                                         [addScope _2 _4] 
                   )
# 81 "Parser.fsy"
                 : 'scopes));
# 380 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            let _4 = (let data = parseState.GetInput(4) in (Microsoft.FSharp.Core.Operators.unbox data : 'scope)) in
            let _6 = (let data = parseState.GetInput(6) in (Microsoft.FSharp.Core.Operators.unbox data : 'scopes)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 82 "Parser.fsy"
                                                              (addScope _2 _4) :: _6 
                   )
# 82 "Parser.fsy"
                 : 'scopes));
# 393 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 85 "Parser.fsy"
                                          ([], _1, []) 
                   )
# 85 "Parser.fsy"
                 : 'scope));
# 404 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 86 "Parser.fsy"
                                                 ([], _1, _2) 
                   )
# 86 "Parser.fsy"
                 : 'scope));
# 416 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definitions)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 87 "Parser.fsy"
                                                   (_1, _2, []) 
                   )
# 87 "Parser.fsy"
                 : 'scope));
# 428 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definitions)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 88 "Parser.fsy"
                                                          (_1, _2, _3) 
                   )
# 88 "Parser.fsy"
                 : 'scope));
# 441 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 91 "Parser.fsy"
                                         [_1] 
                   )
# 91 "Parser.fsy"
                 : 'cconstrs));
# 452 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'cconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 92 "Parser.fsy"
                                                _1 :: _2 
                   )
# 92 "Parser.fsy"
                 : 'cconstrs));
# 464 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 95 "Parser.fsy"
                                           Ast.Multipath 
                   )
# 95 "Parser.fsy"
                 : 'cconstr));
# 474 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 96 "Parser.fsy"
                                                        Ast.MaxRoutes _3 
                   )
# 96 "Parser.fsy"
                 : 'cconstr));
# 485 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 97 "Parser.fsy"
                                                                                     Ast.RouteAggregate(_3, _5, _7) 
                   )
# 97 "Parser.fsy"
                 : 'cconstr));
# 498 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 98 "Parser.fsy"
                                                                                     Ast.CommunityTag(_3, _5, _7) 
                   )
# 98 "Parser.fsy"
                 : 'cconstr));
# 511 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 99 "Parser.fsy"
                                                                   Ast.Ownership(_3, _5) 
                   )
# 99 "Parser.fsy"
                 : 'cconstr));
# 523 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstr)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 102 "Parser.fsy"
                                         [_1] 
                   )
# 102 "Parser.fsy"
                 : 'pconstrs));
# 534 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstr)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'pconstrs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 103 "Parser.fsy"
                                                _1 :: _2 
                   )
# 103 "Parser.fsy"
                 : 'pconstrs));
# 546 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regexs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 106 "Parser.fsy"
                                                     (_1, _3) 
                   )
# 106 "Parser.fsy"
                 : 'pconstr));
# 558 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 109 "Parser.fsy"
                                        [_1] 
                   )
# 109 "Parser.fsy"
                 : 'regexs));
# 569 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regexs)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 110 "Parser.fsy"
                                                _1 :: _3 
                   )
# 110 "Parser.fsy"
                 : 'regexs));
# 581 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 113 "Parser.fsy"
                                     loc _1 
                   )
# 113 "Parser.fsy"
                 : 'regex));
# 592 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 114 "Parser.fsy"
                                                 Ast.Empty 
                   )
# 114 "Parser.fsy"
                 : 'regex));
# 602 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 115 "Parser.fsy"
                                                    Ast.Concat (_1, _3) 
                   )
# 115 "Parser.fsy"
                 : 'regex));
# 614 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 116 "Parser.fsy"
                                                    Ast.Inter (_1, _3) 
                   )
# 116 "Parser.fsy"
                 : 'regex));
# 626 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 117 "Parser.fsy"
                                                 Ast.Union (_1, _3) 
                   )
# 117 "Parser.fsy"
                 : 'regex));
# 638 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 118 "Parser.fsy"
                                           Ast.Negate _2 
                   )
# 118 "Parser.fsy"
                 : 'regex));
# 649 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 119 "Parser.fsy"
                                           Ast.Star _1 
                   )
# 119 "Parser.fsy"
                 : 'regex));
# 660 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'regex)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 120 "Parser.fsy"
                                                  _2 
                   )
# 120 "Parser.fsy"
                 : 'regex));
# 671 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 123 "Parser.fsy"
                                                        Prefix.Prefix(_1, _3, _5, _7, None) 
                   )
# 123 "Parser.fsy"
                 : 'predicate));
# 685 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _5 = (let data = parseState.GetInput(5) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _7 = (let data = parseState.GetInput(7) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            let _9 = (let data = parseState.GetInput(9) in (Microsoft.FSharp.Core.Operators.unbox data : System.Int32)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 124 "Parser.fsy"
                                                                Prefix.Prefix(_1, _3, _5, _7, Some _9) 
                   )
# 124 "Parser.fsy"
                 : 'predicate));
# 700 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 125 "Parser.fsy"
                                       Prefix.True 
                   )
# 125 "Parser.fsy"
                 : 'predicate));
# 710 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 126 "Parser.fsy"
                                        Prefix.False 
                   )
# 126 "Parser.fsy"
                 : 'predicate));
# 720 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 127 "Parser.fsy"
                                                    Prefix.Or(_1, _3) 
                   )
# 127 "Parser.fsy"
                 : 'predicate));
# 732 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            let _3 = (let data = parseState.GetInput(3) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 128 "Parser.fsy"
                                                     Prefix.And(_1, _3) 
                   )
# 128 "Parser.fsy"
                 : 'predicate));
# 744 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 129 "Parser.fsy"
                                              Prefix.Not _2 
                   )
# 129 "Parser.fsy"
                 : 'predicate));
# 755 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'predicate)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 130 "Parser.fsy"
                                                     _2 
                   )
# 130 "Parser.fsy"
                 : 'predicate));
# 766 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definition)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 134 "Parser.fsy"
                                           [_1] 
                   )
# 134 "Parser.fsy"
                 : 'definitions));
# 777 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _1 = (let data = parseState.GetInput(1) in (Microsoft.FSharp.Core.Operators.unbox data : 'definition)) in
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : 'definitions)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 135 "Parser.fsy"
                                                    _1 :: _2 
                   )
# 135 "Parser.fsy"
                 : 'definitions));
# 789 "Parser.fs"
        (fun (parseState : Microsoft.FSharp.Text.Parsing.IParseState) ->
            let _2 = (let data = parseState.GetInput(2) in (Microsoft.FSharp.Core.Operators.unbox data : string)) in
            Microsoft.FSharp.Core.Operators.box
                (
                   (
# 138 "Parser.fsy"
                                           _2 
                   )
# 138 "Parser.fsy"
                 : 'definition));
|]
# 801 "Parser.fs"
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