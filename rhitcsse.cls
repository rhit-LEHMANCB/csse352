% -*- Mode:TeX -*-

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% LaTex format for Computer Science and Software Engineering Courses at 
% Rose-Hulman Institute of Technology
%
% written by jpmellor, 1 March 2000
%
% To use this style - say something like:
%       \documentclass[12pt,twoside]{rhitcsse}
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% Parameters to initialize for boilerplate page:
%
%   \course{CSSE232}
%   \coursename{Computer Architecture I}
%   \term{Winter}
%   \acyear{1999-2000}
%   \title{Sylabus}
%   \instructor{J.P. Mellor}
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% Pagestyle and header generation
%
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

\NeedsTeXFormat{LaTeX2e}[1996/06/01]
\ProvidesClass{rhitcsse}[2000/03/01 v1.0 LaTeX document class for RHIT
CSSE Courses]
\DeclareOption*{\PassOptionsToClass{\CurrentOption}{article}}
\ProcessOptions

% rhitcsse looks much like article
\LoadClass{article}

% we also want the latex2html stuff
%\usepackage{html}
\usepackage{ifthen}

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% Margins.
%
%  Note we want 1in top margin assuming no header line, so push header
%       into 1in margin.
%

\setlength{\oddsidemargin}{0.0in}       % 1in left margin 
\setlength{\evensidemargin}{0.0in}      % 1in left margin (even pages)
\setlength{\topmargin}{0.0in}           % 1in top margin
\setlength{\textwidth}{\paperwidth}     
\addtolength{\textwidth}{-2in}          % Set the width for 1in margins
\setlength{\textheight}{\paperheight}   
\addtolength{\textheight}{-2in}         % Set the height for 1in margins
\addtolength{\topmargin}{-\headheight}  % No header, so compensate
\addtolength{\topmargin}{-\headsep}     % for header height and separation
\setlength{\footskip}{0.5in}            % space for footer

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% I redefine these formats that were defined in article.cls
%
\def\today{\number\day\space\ifcase\month\or
  January\or February\or March\or April\or May\or June\or
  July\or August\or September\or October\or November\or December\fi
  \space\number\year}
%
\if@twoside                                     % If two-sided printing.
\let\@mkboth\markboth
\def\@oddfoot{\scriptsize\today\hfil Page \thepage}
\def\@evenfoot{\scriptsize Page \thepage\hfil\today}
\def\@oddhead{\scriptsize\@term\ \@acyear\hfil\@course}
\def\@evenhead{\scriptsize\@course\hfil\@term\ \@acyear}
\else                                           % If one-sided printing.
\let\@mkboth\markboth
\def\@oddfoot{\scriptsize\today\hfil Page \thepage}
\def\@evenfoot{\scriptsize\today\hfil Page \thepage}
\def\@oddhead{\scriptsize\@term\ \@acyear\hfil\@course}
\def\@evenhead{\scriptsize\@term\ \@acyear\hfil\@course}
\fi

% Something like this might help the spaces between section titles
%\renewcommand\section{\@startsection {section}{1}{\z@}%
%                                   {.1ex \@plus .2ex \@minus .2ex}%
%                                   {.1ex \@plus.2ex}%
%                                   {\normalfont\Large\bfseries}}
\def\section{\@startsection{section}{1}{\z@}%
                                   {-3.5ex \@plus -1ex \@minus -.2ex}%
                                   {0.1ex \@plus .1ex}%
                                   {\normalfont\Large\bfseries}}
\def\subsection{\@startsection{subsection}{2}{\z@}%
                                     {-3.25ex\@plus -1ex \@minus -.2ex}%
                                     {0.1ex \@plus .1ex}%
                                     {\normalfont\large\bfseries}}
\def\subsubsection{\@startsection{subsubsection}{3}{\z@}%
                                     {-3.25ex\@plus -1ex \@minus -.2ex}%
                                     {0.1ex \@plus .1ex}%
                                     {\normalfont\normalsize\bfseries}}
\def\paragraph{\@startsection{paragraph}{4}{\z@}%
                                    {3.25ex \@plus1ex \@minus.2ex}%
                                    {-1em}%
                                    {\normalfont\normalsize\bfseries}}
\def\subparagraph{\@startsection{subparagraph}{5}{\parindent}%
                                       {3.25ex \@plus1ex \@minus .2ex}%
                                       {-1em}%
                                      {\normalfont\normalsize\bfseries}}

\setlength{\parindent}{0in}
\setlength{\parskip}{\baselineskip}

%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
%
% Boilerplate stuff
%
\def\course#1{\gdef\@course{#1}}
\def\@course{}
\def\coursename#1{\gdef\@coursename{#1}}
\def\@coursename{}
\def\term#1{\gdef\@term{#1}}
\def\@term{}
\def\acyear#1{\gdef\@acyear{#1}}
\def\@acyear{}
% \title already exists
\def\instructor#1{\gdef\@instructor{#1}}
\def\@instructor{}

\def\RHIT{Rose-Hulman Institute of Technology}

% Heading for course handouts
\def\maketitle{%
  \begin{center}
    \large
    \@course\ -- \@coursename\\
    \small\RHIT\\
    Computer Science and Software Engineering Department
  \end{center}
  \vspace*{-1.5\parskip}
  \begin{center}
    \large\@title
  \end{center}}

\newcommand{\newpar}{\vspace*{0.3\baselineskip}}
