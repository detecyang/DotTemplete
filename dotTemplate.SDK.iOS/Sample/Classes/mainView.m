//
//  mainView.m
//  dotTemplate
//
//  Created by yang on 13-3-9.
//  Copyright 2013 aisino. All rights reserved.
//

#import "mainView.h"
#import "DotTemplate.h"

#define XML_STRING @"<?xml version=\"1.0\" encoding=\"UTF-8\"?><ROOT>  <TEMPLATE NAME=\"新建模板\" W=\"7\" H=\"10\">    <TEXTBOX ID=\"0\" NAME=\"Txt1\" X=\"1\" Y=\"1\" W=\"5\" H=\"2\" ALIGN=\"L\" />    <TEXTBOX ID=\"1\" NAME=\"Txt2\" X=\"1\" Y=\"4\" W=\"5\" H=\"2\" ALIGN=\"R\" />    <TEXTBOX ID=\"2\" NAME=\"新建文本框2\" X=\"1\" Y=\"7\" W=\"5\" H=\"2\" ALIGN=\"C\" />    </TEMPLATE></ROOT>"


@implementation mainView

// The designated initializer.  Override if you create the controller programmatically and want to perform customization that is not appropriate for viewDidLoad.
/*
- (id)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        // Custom initialization.
    }
    return self;
}
*/

// Implement viewDidLoad to do additional setup after loading the view, typically from a nib.
- (void)viewDidLoad {
    [super viewDidLoad];
    DotTemplate *temp = [[DotTemplate alloc] initWithXmlString:XML_STRING];
    for (TextBox *txt in temp.textBoxArray)
    {
        txt.value = @"awer啊";
    }
    
    NSMutableArray *out = [temp ParseStringsWithTextBox];
    
    NSMutableString *string = [NSMutableString stringWithString:@""];
    for (NSString *str in out)
    {
        [string appendString:str];
        [string appendString:@"\n"];
    }
    lab.text = string;
    NSLog(@"Output:\n%@",string);
}


- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
}

- (void)viewDidUnload {
    [super viewDidUnload];
}

- (void)dealloc {
    [lab release];
    [super dealloc];
}


@end
