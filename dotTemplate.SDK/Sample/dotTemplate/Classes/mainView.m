//
//  mainView.m
//  dotTemplate
//
//  Created by yang on 13-3-9.
//  Copyright 2013 aisino. All rights reserved.
//

#import "mainView.h"
#import "DotTemplate.h"




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
    DotTemplate *temp = [[DotTemplate alloc] init];
    temp.width=10;
    temp.height=3;
    NSMutableArray *array = [[NSMutableArray alloc] initWithCapacity:2];
    TextBox *txt1 = [[TextBox alloc] init];
    txt1.x=0;
    txt1.y=0;
    txt1.width=4;
    txt1.height=2;
    txt1.value = @"123四";
    [array addObject:txt1];
    [txt1 release];
    TextBox *txt2 = [[TextBox alloc] init];
    txt2.x=5;
    txt2.y=0;
    txt2.width=5;
    txt2.height=3;
    txt2.value = @"1四五六七890";
    [array addObject:txt2];
    [txt2 release];
    temp.textBoxArray = array;
    [array release];
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


/*
// Override to allow orientations other than the default portrait orientation.
- (BOOL)shouldAutorotateToInterfaceOrientation:(UIInterfaceOrientation)interfaceOrientation {
    // Return YES for supported orientations.
    return (interfaceOrientation == UIInterfaceOrientationPortrait);
}
*/

- (void)didReceiveMemoryWarning {
    // Releases the view if it doesn't have a superview.
    [super didReceiveMemoryWarning];
    
    // Release any cached data, images, etc. that aren't in use.
}

- (void)viewDidUnload {
    [super viewDidUnload];
    // Release any retained subviews of the main view.
    // e.g. self.myOutlet = nil;
}


- (void)dealloc {
    [lab release];
    [super dealloc];
}


@end
